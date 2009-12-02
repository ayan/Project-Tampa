using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tampa.Interfaces;
using Tampa.Common;

namespace Tampa.InterfaceCompiler
{
    public static class CompilerFactory
    {
        static CompilerFactory()
        {
            _compilers = new Dictionary<CompileTargets, ICompiler>();
        }

        public static ICompiler GetCompiler(CompileTargets target)
        {
            ICompiler compiler;
            if (!_compilers.TryGetValue(target, out compiler))
            {
                switch (target)
                {
                    case CompileTargets.Java: compiler = new JavaCompiler(); break;
                    default: throw new NotImplementedException();
                }

                _compilers[target] = compiler;
            }

            return compiler;
        }

        static Dictionary<CompileTargets, ICompiler> _compilers;
    }
}
