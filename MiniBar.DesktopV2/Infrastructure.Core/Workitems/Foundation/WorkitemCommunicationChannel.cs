using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{
    internal class WorkitemCommunicationChannel : ISubject<WorkitemEventArgs> , IDisposable
    {
        Guid id;
        ReplaySubject<WorkitemEventArgs> _subject = new ReplaySubject<WorkitemEventArgs>();


        public WorkitemCommunicationChannel()
        {
            id = Guid.NewGuid();
            _subject = new ReplaySubject<WorkitemEventArgs>(10);
        }

        public void Dispose()
        {
            _subject.Dispose();
        }

        public void OnCompleted()
        {
            _subject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _subject.OnError(error);
        }

        public void OnNext(WorkitemEventArgs value)
        {
            _subject.OnNext(value);
        }

        IDisposable IObservable<WorkitemEventArgs>.Subscribe(IObserver<WorkitemEventArgs> observer)
        {
            return _subject.Subscribe(observer);
        }
    }
}
