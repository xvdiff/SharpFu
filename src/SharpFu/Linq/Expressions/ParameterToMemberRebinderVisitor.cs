using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpFu.Core.Guarding;

namespace SharpFu.Linq.Expressions
{
	internal class ParameterToMemberRebinderVisitor : ExpressionVisitor
	{

		private readonly ParameterExpression _parameterExpression;
		private readonly MemberExpression _memberExpression;

		public ParameterToMemberRebinderVisitor(ParameterExpression parameterExpression, MemberExpression memberExpression)
		{
			_parameterExpression = parameterExpression;
			_memberExpression = memberExpression;
		}

		public override Expression Visit(Expression node)
		{
			return base.Visit(node == _parameterExpression ? _memberExpression : node);
		}


	}
}
