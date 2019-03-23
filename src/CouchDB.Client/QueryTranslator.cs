﻿using System.Linq.Expressions;
using System.Net.Http;
using System.Text;

namespace CouchDB.Client
{
    internal partial class QueryTranslator : ExpressionVisitor
    {
        private string db;
        private string path;
        private HttpMethod method;
        private StringBuilder sb;

        internal QueryTranslator(string db)
        {
            this.db = db;
        }
        internal CouchRequest Translate(Expression expression)
        {
            this.sb = new StringBuilder();
            sb.Append("{");
            this.Visit(expression);
            sb.Append("}");
            var body = sb.ToString();
            return new CouchRequest(method, path, body);
        }

        protected override Expression VisitLambda<T>(Expression<T> l)
        {
            this.Visit(l.Body);
            return l;
        }
    }
}
