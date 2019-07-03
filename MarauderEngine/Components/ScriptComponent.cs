using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;
using NLua;
using MarauderEngine.Core;

namespace MarauderEngine.Components
{
    public class ScriptComponent: Component<ScriptCData>
    {
        public string ScriptName
        {
            get => _data.ScriptName;
            set => _data.ScriptName = value;
        }

        public override Type type => GetType();
        private Lua state;

        private LuaFunction _initFunction;
        private LuaFunction _updateFunction;
        private LuaFunction _destroyFunction;

        public ScriptComponent(Entity.Entity entity, string path,  string scriptName)
        {
            ScriptName = scriptName;
            RegisterComponent(entity, "ScriptComponent");
            state = new Lua();
            state.DoFile(path + @"\" + ScriptName + ".lua");

            //state.LoadCLRPackage();
            state["Owner"] = Owner; 

            _initFunction = state["Init"] as LuaFunction;
            _updateFunction = state["Update"] as LuaFunction;
            _destroyFunction = state["Destroy"] as LuaFunction;

            _initFunction.Call();
        }


        public override bool FireEvent(Event eEvent)
        {
            return false; 
        }
        
        public override void UpdateComponent()
        {
            
            _updateFunction.Call();

        }

        public override void Destroy()
        {
            _destroyFunction.Call(); 
        }
    }
}
