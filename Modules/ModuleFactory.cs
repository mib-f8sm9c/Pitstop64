using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Modules;

namespace MK64Pitstop.Modules
{
    public static class ModuleFactory
    {
        public enum Modules
        {
            Textures,
            Karts,
            About
        }

        private static Dictionary<Modules, IModule> _modules = new Dictionary<Modules, IModule>();

        public static IModule GetModule(Modules module)
        {
            if(_modules.ContainsKey(module))
                return _modules[module];

            IModule newModule;
            switch(module)
            {
                case Modules.Textures:
                    newModule = new Textures.TexturesModule();
                    break;
                case Modules.Karts:
                    newModule = new Karts.KartModule();
                    break;
                case Modules.About:
                    newModule = new About.AboutModule();
                    break;
                default:
                    newModule = null;
                    break;
            }

            if (newModule != null)
            {
                _modules[module] = newModule;
            }

            return newModule;
        }
    }
}
