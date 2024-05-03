using System.Collections.Generic;
using UnityEngine;

namespace SaintStudio.AssetEvents
{
    public abstract class TypedEvent<TValue, TEvent> : AssetEvent where TEvent : TypedEvent<TValue, TEvent>
    {
        private enum FilterType
        {
            Any,
            None
        }

        [SerializeField] private TValue _value;

        [field: Header("Filter Settings")]
        [Tooltip(
            "When the base event is raised this event is raised as well with the same parameters (if it passes the filter settings as well of course).")]
        [SerializeField]
        private TEvent _baseEvent;

        [Tooltip(
            "In the case of the 'Any' filter the event is raised if the event value is equal to one of the filter values. In the case of the 'None' filter the event is raised if the event value is not equal to any one of the filter values.")]
        [SerializeField]
        private FilterType _filterType = FilterType.None;

        [SerializeField] private List<TValue> _filterValues = new();

        public new TValue Value
        {
            get
            {
                if (_value == null) return default;
                return _value;
            }
            private set => _value = value;
        }

        private void OnEnable()
        {
            _baseEvent?.RegisterHandler(BaseEventHandler);
        }

        private void OnDisable()
        {
            _baseEvent?.RegisterHandler(BaseEventHandler);
        }

        private void BaseEventHandler(AssetEvent data)
        {
            Raise(data.Value);
        }

        public void Raise(TValue value)
        {
            base.Value = value;
            Value = value;

            if (!CheckFilter(value))
            {
                return;
            }

            Raise();
        }

        public override void Raise(object value)
        {
            TValue castValue = value == null ? default :
                value is ValueHolder<TValue> holder ? holder.Value : (TValue)value;

            base.Value = value;
            Value = castValue;

            if (!CheckFilter(castValue))
            {
                return;
            }

            Raise();
        }

        /// <summary>
        /// Raises the event without type checking the provided value.
        /// </summary>
        /// <param name="value"></param>
        public void RaiseBase(object value)
        {
            base.Raise(value);
        }

        private bool CheckFilter(TValue value)
        {
            return _filterType == FilterType.Any && _filterValues.Contains(value) ||
                   _filterType == FilterType.None && !_filterValues.Contains(value);
        }
    }

    public class ValueHolder<T>
    {
        public T Value;

        public static explicit operator T(ValueHolder<T> sh) => sh.Value;
    }
}