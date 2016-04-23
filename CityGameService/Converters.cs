﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityGameService
{
    public static class Converters
    {
        public static gameobj EntityFromDto( GameObjDTO aGameobjDto, gameobj aGameObjectEntity = null)
        {
            gameobj eGO = aGameObjectEntity == null ? new gameobj() : aGameObjectEntity;

            eGO.gameobj_id = aGameobjDto.guid;
            eGO.prefab = aGameobjDto.prefab;
            eGO.layer = aGameobjDto.layer;
            eGO.tag = aGameobjDto.tag;

            if (aGameobjDto.position != null)
            {
                eGO.pos_x = aGameobjDto.position.X;
                eGO.pos_y = aGameobjDto.position.Y;
                eGO.pos_z = aGameobjDto.position.Z;
            }
            if (aGameobjDto.rotation != null)
            {
                eGO.rot_x = aGameobjDto.rotation.X;
                eGO.rot_y = aGameobjDto.rotation.Y;
                eGO.rot_z = aGameobjDto.rotation.Z;
                eGO.rot_w = aGameobjDto.rotation.W;
            }
            if (aGameobjDto.scale != null)
            {
                eGO.scale_x = aGameobjDto.scale.X;
                eGO.scale_y = aGameobjDto.scale.Y;
                eGO.scale_z = aGameobjDto.scale.Z;
            }
            
            /* TODO handle children */
            for (int i=1; 
                   (aGameobjDto.layerChildren != null )
                && (i < aGameobjDto.layerChildren.Count);i++)
            {
                gameobj eChild = new gameobj();
                eChild.gameobj_id = System.Guid.NewGuid().ToString();
                eChild.prefab = @"none";
                eChild.layer = aGameobjDto.layerChildren[i];
                eChild.tag = aGameobjDto.tag;
                eGO.gameobj1.Add(eChild);
            }
            /**/        

            return eGO;
        }

        public static GameObjDTO DtoFromEntity(gameobj aGameobjEntity)
        {
            GameObjDTO goDto = new GameObjDTO();

            Vector3 pos = new Vector3();
            pos.X = aGameobjEntity.pos_x;
            pos.Y = aGameobjEntity.pos_y;
            pos.Z = aGameobjEntity.pos_z;

            Quaternion rot = new Quaternion();
            rot.X = aGameobjEntity.rot_x;
            rot.Y = aGameobjEntity.rot_y;
            rot.Z = aGameobjEntity.rot_z;
            rot.W = aGameobjEntity.rot_w;

            Vector3 scale = new Vector3();
            scale.X = aGameobjEntity.scale_x;
            scale.Y = aGameobjEntity.scale_y;
            scale.Z = aGameobjEntity.scale_z;

            goDto.guid = aGameobjEntity.gameobj_id;
            goDto.prefab = aGameobjEntity.prefab;
            goDto.layer = (Nullable<int>)aGameobjEntity.layer;
            goDto.tag = aGameobjEntity.tag;

            goDto.position = pos;
            goDto.rotation = rot;

            /* TODO handle children */
            for (int i=0; 
                    (aGameobjEntity.gameobj1 != null)
                 && (i< aGameobjEntity.gameobj1.Count); 
                 i++)
            {
                goDto.layerChildren[i] = (int)aGameobjEntity.gameobj1.ElementAt(i).layer;
                goDto.tagsChildren[i] = aGameobjEntity.gameobj1.ElementAt(i).tag;
            }
            /* */
            return goDto;
        }
    }
}