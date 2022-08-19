using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.JavaScript
{
    public class DomQueryable
    {
        private Expression expression;

        private DomQueryable(MemberExpression expression)
        {
            this.expression = expression;
        }

        internal static DomQueryable CreateQuery(MemberExpression expression)
        {
            return new DomQueryable(expression);
        }
        public DomQueryable QuerySelector(string selector)
        {
            expression = Expression.Call(this.expression, typeof(DomQueryable).GetMethod(nameof(QuerySelector)), Expression.Constant(selector));
            return this;
        }
        public void Html(string html)
        {
            expression = Expression.Call(this.expression, typeof(DomQueryable).GetMethod(nameof(Html)), Expression.Constant(html));
            Execute();
        }
        public void Execute()
        {
            var visitor = new DomExpressionVisitor();
            visitor.Visit((MethodCallExpression)expression);
            Runtime.InvokeJS(visitor.JavaScript);
        }

        public T Execute<T>()
        {
            var visitor = new DomExpressionVisitor();
            visitor.Visit((MethodCallExpression)expression);
            var elementInfos = Runtime.InvokeJS(visitor.JavaScript);
            return default;
        }
    }
}
