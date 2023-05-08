using Unity.VisualScripting;
using UnityEngine;

namespace SaintStudio.AssetEvents.VisualScripting
{
    [UnitTitle("On Asset Event")]
    [UnitCategory("Events\\SaintStudio")]
    public class AssetEventUnit : EventUnit<AssetEvent>
    {
        [DoNotSerialize]
        private ValueInput AssetEventReference { get; set; }

        protected override bool register => true;
        
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(UnitEventNames.AssetEvent);
        }

        protected override void Definition()
        {
            base.Definition();

            AssetEventReference = ValueInput<AssetEvent>(nameof(AssetEventReference), null);
        }

        protected override bool ShouldTrigger(Flow flow, AssetEvent assetEvent)
        {
            AssetEvent assignedAssetEvent = flow.GetValue<AssetEvent>(AssetEventReference);

            if (!assignedAssetEvent.TriggerEventUnit)
            {
                Debug.LogError($"{nameof(AssetEventUnit)} :: The asset event unit is referencing an asset event ('{assignedAssetEvent.name}') that doesn't have the {nameof(AssetEvent.TriggerEventUnit)} property set to true. Please set the property to true or remove this asset event reference from the event unit.");
            }
            
            return assignedAssetEvent == assetEvent;
        }
    }
}