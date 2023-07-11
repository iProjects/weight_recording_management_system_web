using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace sqlite_weight_recording_WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface isqlite_service_interface
    {

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        List<weight_record_dto> getallweightsinservice();

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        responsedto createweightinservice(weight_record_dto service_dto);

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        responsedto setup_database();


    }

}
