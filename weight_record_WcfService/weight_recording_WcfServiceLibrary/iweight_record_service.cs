using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using weight_recording_service_dal;

namespace weight_recording_WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface iweight_record_service
    {
        
        // TODO: Add your service operations here

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        List<weight_record_dto> getallweightsinservice();

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        List<weight_record_dto> get_all_active_weights_in_service();

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        responsedto createweightinservice(weight_record_dto service_dto);

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        responsedto updateweightinservice(weight_record_dto service_dto);

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        responsedto deleteweightinservice(weight_record_dto service_dto);

        [OperationContract]
        [FaultContract(typeof(service_fault))]
        responsedto delete_weight_by_changing_status_in_service(weight_record_dto service_dto);
         
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "weight_recording_WcfServiceLibrary.ContractType".
    


}
