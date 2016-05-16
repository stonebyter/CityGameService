using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CityGameService
{
    [ServiceContract]
    public interface IGameObjSvc
    {

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "gameobject/all")]
        SaveGameDto GetAllGO();


        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "gameobject/type/{type}"
            )]
        IList<GameObjDTO> GetGOsByType(string type);

        [OperationContract]
        [WebInvoke(
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "gameobject")]
        void SaveOrUpdateGO(GameObjDTO aGameObjDTO);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "gameobject/delete/{guid}"
            )]
        void DeleteGO(string guid);
    }



    [DataContract]
    public class SaveGameDto
    {
        private List<GameObjDTO> myGameObjects = new List<GameObjDTO>();

        public SaveGameDto(List<GameObjDTO> aGameObjects)
        {
            myGameObjects = aGameObjects;
        }

        [DataMember]
        public List<GameObjDTO> gameObjects
        {
            get { return myGameObjects; }
            set { myGameObjects = value; }
        }
    }

    [DataContract]
    public class GameObjDTO
    {
        private string myPlayer;
        private string myGuid;
        private string myType;
        private string myPrefab;
        private Nullable<int> myLayer;
        private string myTag;
        private Vector3 myPosition;
        private Quaternion myRotation;
        private Vector3 myScale;
        private List<string> myTagsChildren;
        private List<int> myLayerChildren;


        [DataMember]
        public string player
        {
            get { return myPlayer; }
            set { myPlayer = value; }
        }

        [DataMember]
        public string guid
        {
            get { return myGuid; }
            set { myGuid = value; }
        }

        [DataMember]
        public string type
        {
            get { return myType; }
            set { myType = value; }
        }

        [DataMember]
        public string prefab
        {
            get { return myPrefab; }
            set { myPrefab = value; }
        }

        [DataMember]
        public Nullable<int> layer
        {
            get { return myLayer;  }
            set { myLayer = value; }
        }

        [DataMember]
        public string tag
        {
            get { return myTag;  }
            set { myTag = value; }
        }

        [DataMember]
        public Vector3 position
        {
            get { return myPosition; }
            set { myPosition = value; }
        }

        [DataMember]
        public Quaternion rotation
        {
            get { return myRotation; }
            set { myRotation = value; }       
        }

        [DataMember]
        public Vector3 scale
        {
            get { return myScale; }
            set { myScale = value; }
        }

        [DataMember]
        public List<string> tagsChildren
        {
            get { return myTagsChildren;  }
            set { myTagsChildren = value; }
        }

        [DataMember]
        public List<int> layerChildren
        {
            get { return myLayerChildren; }
            set { myLayerChildren = value; }
        }
    }

    [Serializable]
    public class Vector3
    {
        private Nullable<double> x;
        private Nullable<double> y;
        private Nullable<double> z;

        [DataMember]
        public Nullable<double> X
        {
            get { return x; }
            set { x = value; }
        }

        [DataMember]
        public Nullable<double> Y
        {
            get { return y; }
            set { y = value; }
        }

        [DataMember]
        public Nullable<double> Z
        {
            get { return z; }
            set { z = value; }
        }
    }

    [Serializable]
    public class Quaternion
    {
        private Nullable<double> x;
        private Nullable<double> y;
        private Nullable<double> z;
        private Nullable<double> w;


        [DataMember]
        public Nullable<double> X
        {
            get { return x; }
            set { x = value; }
        }

        [DataMember]
        public Nullable<double> Y
        {
            get { return y; }
            set { y = value; }
        }

        [DataMember]
        public Nullable<double> Z
        {
            get { return z; }
            set { z = value; }
        }

        [DataMember]
        public Nullable<double> W
        {
            get { return w; }
            set { w = value; }
        }
    }
}
