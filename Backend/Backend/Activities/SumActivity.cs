using Backend.Models;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Models;

namespace Backend.Activities
{
    public class SumAct : CodeActivity<int>
    {
        DbContextClasse _context = new DbContextClasse();
        public SumAct(Variable<int> a, Variable<int> b)
        {
            A = new(a);
            B = new(b);
       
        }

        public Input<int> A { get; set; }
        public Input<int> B { get; set; }
        protected override void Execute(ActivityExecutionContext context)
        {
      
            var input1 = A.Get(context);
            var input2 = B.Get(context);
            var result = input1 + input2;
    
            Result.Set(context, result);

         }
       
    }
}
