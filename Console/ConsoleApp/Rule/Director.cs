using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Rule
{
    public class Director : AbstractAuditor
    {
        public override void Audit(ApplyContext applyContext)
        {
            Console.WriteLine($"this is { this.GetType().Name } {this.Name} 来审核");
            if (applyContext.Hour <= 8)
            {
                applyContext.AuditResult = true;
            }
            else
            {
                //if (this._NextAuditor != null)
                //{
                //    this._NextAuditor.Audit(applyContext);
                //}

                AuditNext(applyContext);
            }
        }
    }
} 
