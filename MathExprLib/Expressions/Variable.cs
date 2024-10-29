public class Variable : IExpr
{
    public string Name { get; }

    public Variable(string name) => Name = name;

    public IEnumerable<string> Variables => new[] { Name };
    public bool IsConstant => false;
    public bool IsPolynomial => true;
        public int PolynomialDegree => 1;

    public double Compute(IReadOnlyDictionary<string, double> variableValues) =>
        variableValues.ContainsKey(Name) ? variableValues[Name] : throw new ArgumentException("Variable not defined");

    public IExpr Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    public override string ToString() => Name;
}
