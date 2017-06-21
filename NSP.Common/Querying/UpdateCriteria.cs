using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Querying
{
    public sealed class UpdateCriteria<TTableObject> : IDictionary<string, object> where TTableObject : class, new()
    {
        private class DumpMemberAccessNameVisitor : ExpressionVisitor
        {
            private List<string> nameList = new List<string>();
            protected override Expression VisitMember(MemberExpression node)
            {
                var expression = base.VisitMember(node);
                nameList.Add(node.Member.Name);
                return expression;
            }

            public string MemberAccessName => string.Join(".", nameList);

            public override string ToString()
            {
                return MemberAccessName;
            }
        }

        private readonly Dictionary<string, object> _updateCriterias = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                return _updateCriterias[key];
            }
            set
            {
                _updateCriterias[key] = value;
            }
        }

        public int Count => _updateCriterias.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => _updateCriterias.Keys;

        public ICollection<object> Values => _updateCriterias.Values;

        private static Expression<Func<TTableObject, object>> CreateLambdaExpression(string propertyName)
        {
            var param = Expression.Parameter(typeof(TTableObject), "x");
            Expression body = param;

            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.Property(body, member);
            }
            return Expression.Lambda<Func<TTableObject, object>>(Expression.Convert(body, typeof(object)), param);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, object value)
        {
            _updateCriterias.Add(key, value);
        }

        public void Add(Expression<Func<TTableObject, object>> updateCriteria, object value)
        {
            var visitor = new DumpMemberAccessNameVisitor();
            visitor.Visit(updateCriteria);
            var memberAccessName = visitor.MemberAccessName;
            if (!ContainsKey(memberAccessName))
            {
                Add(memberAccessName, value);
            }
        }

        public IEnumerable<Tuple<Expression<Func<TTableObject, object>>, object>> UpdateCriterias
        {
            get
            {
                foreach (var kvp in _updateCriterias)
                {
                    yield return new Tuple<Expression<Func<TTableObject, object>>, object>(CreateLambdaExpression(kvp.Key), kvp.Value);
                }
            }
        }

        public void Clear()
        {
            _updateCriterias.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _updateCriterias.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _updateCriterias.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection)_updateCriterias).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _updateCriterias.Remove(item.Key);
        }
        public bool Remove(string key)
        {
            return _updateCriterias.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _updateCriterias.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _updateCriterias.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _updateCriterias.GetEnumerator();
        }
    }
}
