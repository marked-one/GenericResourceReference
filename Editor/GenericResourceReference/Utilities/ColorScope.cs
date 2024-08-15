using System;
using UnityEngine;

namespace GenericResourceReference {
    public class ColorScope : IDisposable {
        readonly Color _originalColor;
        readonly bool _condition;

        public ColorScope(Color color, bool condition) {
            _condition = condition;
            if (!_condition)
                return;

            _originalColor = GUI.color;
            GUI.color = color;
        }

        public void Dispose() {
            if (_condition)
                GUI.color = _originalColor;
        }
    }
}