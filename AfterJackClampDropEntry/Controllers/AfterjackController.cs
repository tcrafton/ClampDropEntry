using AfterJackClampDropEntry.Models;
using AfterJackClampDropEntry.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AfterJackClampDropEntry.Controllers
{
    public class AfterjackController : ApiController
    {
        DataRepo afterJacks = new DataRepo();

        [ActionName("GetAfterJacks")]
        public DataTable GetAfterJacks(DateTime entryDate, string room)
        {            
            return afterJacks.GetAfterjacks(entryDate, room);
        }

        [ActionName("PostAfterjack")]
        [HttpPost]
        public void PostAfterJack(AfterJack afterJack)
        {
            afterJacks.UpdateAfterJack(afterJack);
        }

        [ActionName("PutAfterjacks")]
        [HttpPut]
        public void PutAfterJacks(AfterJackDetail[] afterJacksDetails)
        {
            afterJacks.UpdateAfterJackDetail(afterJacksDetails);
        }

        [ActionName("DeleteAfterjack")]
        [HttpDelete]
        public void DeleteAfterJack(int id, int afterjackID)
        {
            afterJacks.DeleteAfterJacks(id, afterjackID);
        }
    }
}
