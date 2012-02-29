using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace SpinPlatform
{
    
    namespace Data
    {
        public class SharedData <T>
        {
            //mienbros privados
           List<T> _lista;//Lista con los datos

            Mutex _mutex;
            readonly object _locker ;
            int _elementos; // numero de elementos en la lista
            bool _lleno; // señal de lista vacia 
            bool _vacio;// señal de lista llena
            int _maxelem; // max elementos de la lista
           


            //descriptores de acceso
            public int Elementos
            {
                get
                {
                    _mutex.WaitOne();
                    int elementos = _elementos;
                    _mutex.ReleaseMutex();//Fin Seccion Critica
                    return elementos;
                }
                   
            }
            public bool LLeno { get {
                _mutex.WaitOne();
                bool lleno = _lleno;
                _mutex.ReleaseMutex();//Fin Seccion Critica
                return _lleno;
            } }
            public bool Vacio {

                get
                {
                    _mutex.WaitOne();
                    bool vacio = _vacio;
                    _mutex.ReleaseMutex();//Fin Seccion Critica
                return _vacio; } 
            }
            public int MaxElem {
                get {
                    _mutex.WaitOne();
                    int maxelem = _maxelem;
                    _mutex.ReleaseMutex();//Fin Seccion Critica
                    
                    return _maxelem; } }

            //metodos
            public SharedData(int MaxElem)
            {
                _lista = new List<T>(MaxElem);
                _locker = new object();
                _mutex = new Mutex();
                _maxelem = MaxElem;
                _lleno = false;
                _vacio = true;
                _elementos = 0;

            }
            public void Reset()
            {
                lock (_locker)
                {
                    _lista.Clear();
                    _maxelem = MaxElem;
                    _lleno = false;
                    _vacio = true;
                    _elementos = 0; 
                }



            }
            public SharedData<T> Copy()
            {
                return (SharedData<T>)this.MemberwiseClone();
            }
            public void Add(T obj)
            {

                lock (_locker)
                {//Inicio Seccion Critica

                    if (_lleno)
                    {
                        _lista.RemoveAt(0);

                    }
                    _lista.Add(obj);
                    _elementos = _lista.Count;
                    if (_elementos >= _maxelem) _lleno = true;
                    _vacio = false;

                }//Fin Seccion Critica
                

            }
            public object Pop()
            {
                if (Elementos > 0)
                {
                    object elemento;
                    lock (_locker)
                    {//Inicio Seccion Critica

                       elemento = (object)_lista[0];

                        _lista.RemoveAt(0);
                        _elementos = _lista.Count;

                        if (_lista.Count == 0) _vacio = true;
                        else _vacio = false;
                        _lleno = false;

                    }//Fin Seccion Critica

                    return elemento;
                }
                else return null;


            }
            public object Get(int index)
            {
                if (Elementos > index)
                {
                    object elemento;
                    lock (_locker)
                    {//Inicio Seccion Critica

                        elemento = (object)_lista[index];
                    }//Fin Seccion Critica

                    return elemento;
                }
                else return null;

            }
            public void Set(int index, T element)
            {
                lock (_locker)
                {//Inicio Seccion Critica
                    if (Elementos > index)
                    {

                        _lista[index] = element;



                    }
                    else if (Elementos == 0)
                    {

                        _lista.Add(element);
                        _elementos = _lista.Count;
                        if (_elementos >= _maxelem) _lleno = true;
                        _vacio = false;

                    }
                }//Fin Seccion Critica



            }
        }
    }
}
