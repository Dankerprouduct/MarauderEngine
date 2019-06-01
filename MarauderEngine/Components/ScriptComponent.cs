using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using MarauderEngine.Core;

namespace MarauderEngine.Components
{
    public class ScriptComponent: IComponent
    {
        public Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        private Lua state;

        private LuaFunction _initFunction;
        private LuaFunction _updateFunction;
        private LuaFunction _destroyFunction;

        public ScriptComponent(Entity.Entity entity, string path,  string scriptName)
        {
            RegisterComponent(entity, "ScriptComponent");
            state = new Lua();
            state.DoFile(path + @"\" +scriptName + ".lua");

            //state.LoadCLRPackage();
            state["Owner"] = Owner; 

            _initFunction = state["Init"] as LuaFunction;
            _updateFunction = state["Update"] as LuaFunction;
            _destroyFunction = state["Destroy"] as LuaFunction;

            _initFunction.Call();
        }

        public void RegisterComponent(Entity.Entity entity, string componentName)
        {
            Owner = entity;
            Name = componentName; 
        }

        public bool FireEvent(Event eEvent)
        {
            return false; 
        }
        
        public void UpdateComponent()
        {
            
            _updateFunction.Call();

        }

        public void Destroy()
        {
            _destroyFunction.Call(); 
        }
    }
}
