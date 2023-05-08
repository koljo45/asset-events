using Unity.VisualScripting;
using UnityEngine;

namespace SaintStudio.AssetEvents.VisualScripting
{
    [UnitTitle("On Object Filtered Event")]
    [UnitCategory("Events\\SaintStudio")]
    public class ObjectFilteredEventUnit : EventUnit<ObjectFilteredEventUnit.ObjectFilteredEventArgs>
    {
        public struct ObjectFilteredEventArgs
        {
            public Object Source;
            public object Arg0;
        }
        
        [DoNotSerialize]
        private ValueInput ObjectReference { get; set; }
        [DoNotSerialize]
        private ValueOutput Arg0 { get; set; }

        protected override bool register => true;
        
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(UnitEventNames.ObjectFilteredEvent);
        }

        protected override void Definition()
        {
            base.Definition();

            ObjectReference = ValueInput<Object>(nameof(ObjectReference), null);
            
            Arg0 = ValueOutput<object>(nameof(Arg0));
        }
        
        protected override void AssignArguments(Flow flow, ObjectFilteredEventArgs data)
        {
            flow.SetValue(Arg0, data.Arg0);
        }

        protected override bool ShouldTrigger(Flow flow, ObjectFilteredEventArgs args)
        {
            return flow.GetValue<Object>(ObjectReference) == args.Source;
        }
    }
}