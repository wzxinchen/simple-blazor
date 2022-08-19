using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.JavaScript
{
    internal class DomExpressionVisitor
    {
        public string JavaScript { get; private set; }
        private Stack<string> _scripts = new Stack<string>();
        public void Visit(MethodCallExpression expression)
        {
            VisitExpression(expression);
            JavaScript = string.Join(".", _scripts);
            Console.WriteLine(JavaScript);
        }

        private void VisitExpression(MethodCallExpression expression)
        {
            if (expression.Method.Name == nameof(DomQueryable.Html))
            {
                VisitHtml((ConstantExpression)expression.Arguments[0]);
            }
            else if (expression.Method.Name == nameof(DomQueryable.QuerySelector))
            {
                VisitQuerySelector((ConstantExpression)expression.Arguments[0]);
            }
            if (expression.Object is MethodCallExpression methodCallExpression)
            {
                VisitExpression(methodCallExpression);
            }
            else if (expression.Object is MemberExpression memberExpression)
            {
                if (memberExpression.Expression == null && memberExpression.Member.Name == nameof(Document.Body))
                {
                    VisitBody();
                }
            }
        }

        protected virtual void VisitQuerySelector(ConstantExpression constantExpression)
        {
            _scripts.Push($"querySelector('{constantExpression.Value.ToString()}')");
        }

        protected virtual void VisitBody()
        {
            _scripts.Push($"document.body");
        }

        protected virtual void VisitHtml(ConstantExpression constantExpression)
        {
            _scripts.Push($"innerHTML=`{constantExpression.Value?.ToString()}`");
        }
    }
}
