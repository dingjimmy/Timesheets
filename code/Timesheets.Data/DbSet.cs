using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Timesheets.Data
{

    /// <summary>
    /// Represents a 'faked' or 'mocked' base class to use when mocking a DbContext when testing/demonstrating.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {

        private ObservableCollection<T> data;
        private IQueryable query;

        public FakeDbSet()
        {
            this.data = new ObservableCollection<T>();
            this.query = data.AsQueryable();
        }

        public T Add(T entity)
        {
            this.data.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            this.data.Add(entity);
            return entity;
        }

        TDerivedEntity IDbSet<T>.Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get
            {
                return this.data;
            }
        }

        public T Remove(T entity)
        {
            this.data.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return this.query.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this.query.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this.query.Provider;
            }
        }

    }
}
