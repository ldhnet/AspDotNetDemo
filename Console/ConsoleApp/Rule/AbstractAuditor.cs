using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Rule
{
    public abstract class AbstractAuditor
    {
        public string Name { get; set; }
        public AbstractAuditor _NextAuditor = null;
        public void SetNextAuditor(AbstractAuditor abstractAuditor)
        { 
            this._NextAuditor = abstractAuditor;    
        }
        public abstract void Audit(ApplyContext applyContext);

        public  void AuditNext(ApplyContext applyContext) {
            if (_NextAuditor != null)
            {
                _NextAuditor.Audit(applyContext);
            }
        }
    }
}
