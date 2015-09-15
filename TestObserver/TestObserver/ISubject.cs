using System;
namespace TestObserver
{
    interface ISubject
    {
        void AddOvserv(IObserver l_observ);
        void DelOvserv(IObserver l_observ);
        void SetChanged();
    }
}
