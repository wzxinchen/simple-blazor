using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.JavaScript
{
    public class Document
    {
        public static DomQueryable Body
        {
            get
            {
                var bodyProperty = typeof(Document).GetProperty(nameof(Body));
                var propertyExpression = Expression.Property(null, bodyProperty);
                return DomQueryable.CreateQuery(propertyExpression);
            }
        }
    }
}
