using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    [UsedImplicitly]
    internal sealed class DeviceSimulatorDeviceSpecificationPlugin : DeviceSimulatorPlugin
    {
        public override string title => "Device Specification";

        private Label m_resolutionLabel;
        private Label m_safeAreaLabel;

        private Resolution m_currentCurrentResolution;
        private Rect       m_currentSafeArea;

        public override VisualElement OnCreateUI()
        {
            EditorApplication.update -= Update;
            EditorApplication.update += Update;

            var root = new VisualElement();

            m_resolutionLabel = new Label();
            m_safeAreaLabel   = new Label();

            root.Add( m_resolutionLabel );
            root.Add( m_safeAreaLabel );

            Update();

            return root;
        }

        private void Update()
        {
            var currentResolution = UnityEngine.Device.Screen.currentResolution;
            var safeArea          = UnityEngine.Device.Screen.safeArea;

            if ( m_currentCurrentResolution.width == currentResolution.width &&
                 m_currentCurrentResolution.height == currentResolution.height &&
                 m_currentCurrentResolution.refreshRate == currentResolution.refreshRate &&
                 m_currentSafeArea == safeArea )
            {
                return;
            }

            m_resolutionLabel.text = $"Resolution: {currentResolution.width.ToString()} x {currentResolution.height.ToString()}";
            m_safeAreaLabel.text   = $"Safe Area: x: {safeArea.x:0}, y: {safeArea.y:0}, width: {safeArea.width:0}, height: {safeArea.height:0}";

            m_currentCurrentResolution = currentResolution;
            m_currentSafeArea          = safeArea;
        }
    }
}