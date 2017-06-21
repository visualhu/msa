using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Querying
{
    public abstract class WhereClauseBuilder<TTableObject> : ExpressionVisitor, IWhereClauseBuilder<TTableObject> where TTableObject : class, new()
    {
        private readonly StringBuilder sb = new StringBuilder();
        private readonly Dictionary<string, object> parameterValues = new Dictionary<string, object>();
        private bool startsWith = false;
        private bool endsWith = false;
        private bool contains = false;

        public WhereClauseBuilder() { }

        private void Out(string s)
        {
            sb.Append(s);
        }

        private void OutMember(Expression instance, MemberInfo member)
        {
            string mappedFieldName = member.Name;
            Out(mappedFieldName);
        }

        protected virtual string And => "AND";
        protected virtual string Or => "OR";
        protected virtual string Equal => "=";
        protected virtual string Not => "NOT";
        protected virtual string NotEqual => "<>";
        protected virtual string Like => "LIKE";

        protected virtual char LikeSymbol => '%';
        protected internal abstract char ParameterChar { get; }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            string str;
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    str = "+";
                    break;
                case ExpressionType.AddChecked:
                    str = "+";
                    break;
                case ExpressionType.AndAlso:
                    str = this.And;
                    break;
                case ExpressionType.Divide:
                    str = "/";
                    break;
                case ExpressionType.Equal:
                    str = this.Equal;
                    break;
                case ExpressionType.GreaterThan:
                    str = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    str = ">=";
                    break;
                case ExpressionType.LessThan:
                    str = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    str = "<=";
                    break;
                case ExpressionType.Modulo:
                    str = "%";
                    break;
                case ExpressionType.Multiply:
                    str = "*";
                    break;
                case ExpressionType.MultiplyChecked:
                    str = "*";
                    break;
                case ExpressionType.Not:
                    str = this.Not;
                    break;
                case ExpressionType.NotEqual:
                    str = this.NotEqual;
                    break;
                case ExpressionType.OrElse:
                    str = this.Or;
                    break;
                case ExpressionType.Subtract:
                    str = "-";
                    break;
                case ExpressionType.SubtractChecked:
                    str = "-";
                    break;
                default:
                    throw new NotSupportedException($"Node type {node.NodeType.ToString()} is not supported.");
            }
            Out("(");
            Visit(node.Left);
            Out(" ");
            Out(str);
            Out(" ");
            Visit(node.Right);
            Out(")");
            return node;

        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TTableObject)
                || typeof(TTableObject).IsSubclassOf(node.Member.DeclaringType))
            {
                string mappedFieldName = node.Member.Name;
                Out(mappedFieldName);
            }
            else
            {
                if (node.Member is FieldInfo)
                {
                    ConstantExpression ce = node.Expression as ConstantExpression;
                    FieldInfo fi = node.Member as FieldInfo;
                    object fieldValue = fi.GetValue(ce.Value);
                    Expression constantExpr = Expression.Constant(fieldValue);
                    Visit(constantExpr);
                }
                else
                {
                    throw new NotSupportedException($"Member type {node.Member.GetType().FullName} is not supported");
                }
            }
            return node;

        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            string paramName = string.Format("{0}{1}", ParameterChar, Utils.GetUniqueIdentifier(5));
            Out(paramName);
            if (!parameterValues.ContainsKey(paramName))
            {
                object v = null;
                if (startsWith && node.Value is string)
                {
                    startsWith = false;
                    v = node.Value.ToString() + LikeSymbol;
                }
                else if (endsWith && node.Value is string)
                {
                    endsWith = false;
                    v = LikeSymbol + node.Value.ToString();
                }
                else if (contains && node.Value is string)
                {
                    contains = false;
                    v = LikeSymbol + node.Value.ToString() + LikeSymbol;
                }
                else
                {
                    v = node.Value;
                }
                parameterValues.Add(paramName, v);
            }
            return node;
        }


        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Out("(");
            Visit(node.Object);
            if (node.Arguments == null || node.Arguments.Count != 1)
                throw new NotSupportedException("Argument length is not valid.");
            Expression expr = node.Arguments[0];
            switch (node.Method.Name)
            {
                case "StartsWith":
                    startsWith = true;
                    Out(" ");
                    Out(Like);
                    Out(" ");
                    break;
                case "EndsWith":
                    endsWith = true;
                    Out(" ");
                    Out(Like);
                    Out(" ");
                    break;
                case "Equals":
                    Out(" ");
                    Out(Equal);
                    Out(" ");
                    break;
                case "Contains":
                    contains = true;
                    Out(" ");
                    Out(Like);
                    Out(" ");
                    break;
                default:
                    throw new NotSupportedException($"Method {node.Method.Name} is not supported.");
            }
            if (expr is ConstantExpression || expr is MemberExpression)
                Visit(expr);
            else
                throw new NotSupportedException();
            Out(")");
            return node;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitDynamic(System.Linq.Expressions.DynamicExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitExtension(Expression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitNew(NewExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }


        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            throw new NotSupportedException($"Node type {node.GetType().Name} is not supported.");
        }

        public WhereClauseBuildResult BuildWhereClause(Expression<Func<TTableObject, bool>> expression)
        {
            this.sb.Clear();
            this.parameterValues.Clear();
            this.Visit(expression.Body);

            WhereClauseBuildResult result = new WhereClauseBuildResult
            {
                ParameterValues = parameterValues,
                WhereClause = sb.ToString()
            };
            return result;

        }
    }
}
