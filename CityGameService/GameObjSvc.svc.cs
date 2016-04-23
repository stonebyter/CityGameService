using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CityGameService
{
    public class GameObjSvc : IGameObjSvc
    {
        public IList<GameObjDTO> GetUserCreatedGOs()
        {
            List<GameObjDTO> res = new List<GameObjDTO>();
            try
            {
                using (var repo = new CityGameDBModel())
                {
                    player player = repo.player.Where<player>(p => p.login.Equals("nomitrey")).FirstOrDefault<player>();

                    List<gameobj> goEntities = repo.gameobj.Where<gameobj>(go => go.player_id == player.player_id).ToList<gameobj>();
                    foreach (var eGO in goEntities)
                    {
                        res.Add(Converters.DtoFromEntity(eGO));
                    }              
                }
            }
            catch (Exception e)
            { 
                System.Diagnostics.Debug.WriteLine("Exception: " + e);
            }

            return res;
        }

        public GameObjDTO SaveUserCreatedGO(GameObjDTO composite)
        {
            try
            {
                using (var repo = new CityGameDBModel())
                {
                    player player = repo.player.Where<player>( p => p.login.Equals(composite.player)).FirstOrDefault<player>();

                    if (player!=null)
                    {
                        gameobj go = repo.gameobj.Where<gameobj>(g => g.gameobj_id.Equals(composite.guid)).FirstOrDefault<gameobj>();
                        if (go == null)
                        {   // We have a new record...
                            go = Converters.EntityFromDto(composite);
                            repo.gameobj.Add(go);
                        }
                        else
                        {   // We have an existing record...
                            go = Converters.EntityFromDto(composite, go);
                            repo.Entry(go).State = System.Data.Entity.EntityState.Modified;
                        }

                        go.player_id = player.player_id;
                        repo.SaveChanges();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e);                
            }
            return composite;
        }
    }
}
