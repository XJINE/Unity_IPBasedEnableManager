using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace IPBasedEnableManagers
{
    public class IPBasedEnableManager : MonoBehaviour
    {
        public enum StartAction
        {
            DoNothing,
            EnableTargets,
            DisableTargets,
        }
    
        #region Field

        [SerializeField] private bool forceActionInEditor = true;

        public StartAction      startAction;
        public List<string>     ipAddresses;
        public List<GameObject> targetObjects;
        public List<Behaviour>  targetBehaviours;
    
        #endregion Field
    
        #region Method
    
        public void Start()
        {
            switch (startAction)
            {
                case StartAction.DisableTargets : DisableTargets(); break;
                case StartAction.EnableTargets  : EnableTargets();  break;
                case StartAction.DoNothing      :                   break;
                default:break;
            }
        }
    
        public void EnableTargets () { SetEnabled(true);  }
        public void DisableTargets() { SetEnabled(false); }
    
        private void SetEnabled(bool enabled)
        {
            foreach (var localAddress in Dns.GetHostEntry("").AddressList)
            {
                foreach (var ipAddress in ipAddresses.Where(ipAddress => Equals(localAddress, IPAddress.Parse(ipAddress))))
                {
                    SetEnabledLocal(enabled);
                }
            }

            if (forceActionInEditor && Application.isEditor)
            {
                SetEnabledLocal(enabled);
            }

            void SetEnabledLocal(bool enabled)
            {
                foreach (var targetBehaviour in targetBehaviours)
                {
                    targetBehaviour.enabled = enabled;
                }
    
                foreach (var targetObject in targetObjects)
                {
                    targetObject.SetActive(enabled);
                }
            }
        }
    
        #endregion Method
    }
}