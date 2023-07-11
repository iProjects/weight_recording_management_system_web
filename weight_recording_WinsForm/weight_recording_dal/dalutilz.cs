using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace weight_recording_dal
{
    public class dalutilz
    {
        public dalutilz()
        {

        }

        public static List<weight_record_service.weight_record_dto> getallweightsfromservice()
        {
            List<weight_record_service.weight_record_dto> dtos = new List<weight_record_service.weight_record_dto>();
            try
            {
                using (weight_record_service.iweight_record_serviceClient service = new weight_record_service.iweight_record_serviceClient())
                {
                    var lst = service.getallweightsinservice();

                    foreach (var dto in lst)
                    {
                        weight_record_service.weight_record_dto weight_dto = new weight_record_service.weight_record_dto();
                        weight_dto.weight_id = dto.weight_id;
                        weight_dto.weight_weight = dto.weight_weight;
                        weight_dto.weight_date = dto.weight_date;
                        weight_dto.weight_status = dto.weight_status;
                        weight_dto.created_date = dto.created_date;
                        weight_dto.weight_app = dto.weight_app;

                        dtos.Add(weight_dto);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
            return dtos;
        }
        public static List<weight_record_service.weight_record_dto> get_all_active_weights_from_service()
        {
            List<weight_record_service.weight_record_dto> dtos = new List<weight_record_service.weight_record_dto>();
            try
            {
                using (weight_record_service.iweight_record_serviceClient service = new weight_record_service.iweight_record_serviceClient())
                {
                    var lst = service.get_all_active_weights_in_service();

                    foreach (var dto in lst)
                    {
                        weight_record_service.weight_record_dto weight_dto = new weight_record_service.weight_record_dto();
                        weight_dto.weight_id = dto.weight_id;
                        weight_dto.weight_weight = dto.weight_weight;
                        weight_dto.weight_date = dto.weight_date;
                        weight_dto.weight_status = dto.weight_status;
                        weight_dto.created_date = dto.created_date;
                        weight_dto.weight_app = dto.weight_app;

                        dtos.Add(weight_dto);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
            return dtos;
        }
        public static List<weight_ui_dto> getallweightsforui()
        {
            List<weight_record_service.weight_record_dto> lst_weight_service_dto = getallweightsfromservice();
            List<weight_ui_dto> lst_weight_ui_dto = lstconvertservicedtointouidto(lst_weight_service_dto);

            return lst_weight_ui_dto;
        }

        public weight_ui_dto convertservicedtointouidto(weight_record_service.weight_record_dto weight_service_dto)
        {
            weight_ui_dto _weight_ui_dto = new weight_ui_dto();
            _weight_ui_dto.weight_id = weight_service_dto.weight_id;
            _weight_ui_dto.weight_weight = weight_service_dto.weight_weight;
            _weight_ui_dto.weight_date = weight_service_dto.weight_date;
            _weight_ui_dto.weight_status = weight_service_dto.weight_status;
            _weight_ui_dto.created_date = weight_service_dto.created_date;
            _weight_ui_dto.weight_app = weight_service_dto.weight_app;

            return _weight_ui_dto;
        }

        public static List<weight_ui_dto> lstconvertservicedtointouidto(List<weight_record_service.weight_record_dto> lst_weight_service_dto)
        {
            List<weight_ui_dto> lst_weight_ui_dto = new List<weight_ui_dto>();
            foreach (var weight_service_dto in lst_weight_service_dto)
            {
                weight_ui_dto _weight_ui_dto = new weight_ui_dto();
                _weight_ui_dto.weight_id = weight_service_dto.weight_id;
                _weight_ui_dto.weight_weight = weight_service_dto.weight_weight;
                _weight_ui_dto.weight_date = weight_service_dto.weight_date;
                _weight_ui_dto.weight_status = weight_service_dto.weight_status;
                _weight_ui_dto.created_date = weight_service_dto.created_date;
                _weight_ui_dto.weight_app = weight_service_dto.weight_app;

                lst_weight_ui_dto.Add(_weight_ui_dto);
            }

            return lst_weight_ui_dto;
        }
        public static weight_record_service.weight_record_dto convertuidtointoservicedto(weight_ui_dto _weight_ui_dto)
        {
            weight_record_service.weight_record_dto weight_service_dto = new weight_record_service.weight_record_dto();
            weight_service_dto.weight_id = _weight_ui_dto.weight_id;
            weight_service_dto.weight_weight = _weight_ui_dto.weight_weight;
            weight_service_dto.weight_date = _weight_ui_dto.weight_date;
            weight_service_dto.weight_status = _weight_ui_dto.weight_status;
            weight_service_dto.created_date = _weight_ui_dto.created_date;
            weight_service_dto.weight_app = _weight_ui_dto.weight_app;

            return weight_service_dto;
        }
        public static responsedto convertserviceresponsedtointouiresponsedto(weight_record_service.responsedto weight_service_record_dto)
        {
            responsedto ui_responsedto = new responsedto();
            ui_responsedto.isresponseresultsuccessful = weight_service_record_dto.isresponseresultsuccessful;
            ui_responsedto.responseclass = weight_service_record_dto.responseclass;
            ui_responsedto.responseerrormessage = weight_service_record_dto.responseerrormessage;
            ui_responsedto.responsemethod = weight_service_record_dto.responsemethod;
            ui_responsedto.responseresultobject = weight_service_record_dto.responseresultobject;
            ui_responsedto.responsesuccessmessage = weight_service_record_dto.responsesuccessmessage;

            return ui_responsedto;
        }
        public static responsedto createweightingservice(weight_ui_dto dto)
        {
            responsedto response_dto = new responsedto();
            try
            {
                using (weight_record_service.iweight_record_serviceClient service = new weight_record_service.iweight_record_serviceClient())
                {
                    weight_record_service.weight_record_dto service_dto = convertuidtointoservicedto(dto);
                    weight_record_service.responsedto service_responsedto = service.createweightinservice(service_dto);
                    response_dto = convertserviceresponsedtointouiresponsedto(service_responsedto);
                }
            }
            catch (Exception ex)
            {
                response_dto.responseerrormessage += ex.Message;
                Log.WriteToErrorLogFile(ex);
            }
            return response_dto;
        }
        public static responsedto updateweightingservice(weight_ui_dto dto)
        {
            responsedto response_dto = new responsedto();
            try
            {
                using (weight_record_service.iweight_record_serviceClient service = new weight_record_service.iweight_record_serviceClient())
                {
                    weight_record_service.weight_record_dto service_dto = convertuidtointoservicedto(dto);
                    weight_record_service.responsedto service_responsedto = service.updateweightinservice(service_dto);
                    response_dto = convertserviceresponsedtointouiresponsedto(service_responsedto);
                }
            }
            catch (Exception ex)
            {
                response_dto.responseerrormessage += ex.Message;
                Log.WriteToErrorLogFile(ex);
            }
            return response_dto;
        }
        public static responsedto deleteweightingservice(weight_ui_dto dto)
        {
            responsedto response_dto = new responsedto();
            try
            {
                using (weight_record_service.iweight_record_serviceClient service = new weight_record_service.iweight_record_serviceClient())
                {
                    weight_record_service.weight_record_dto service_dto = convertuidtointoservicedto(dto);
                    weight_record_service.responsedto service_responsedto = service.delete_weight_by_changing_status_in_service(service_dto);
                    response_dto = convertserviceresponsedtointouiresponsedto(service_responsedto);
                }
            }
            catch (Exception ex)
            {
                response_dto.responseerrormessage += ex.Message;
                Log.WriteToErrorLogFile(ex);
            }
            return response_dto;
        }



    }
}
