using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SocialMediaAccess
{
    public class TwitterServices : ITwitterServices
    {
        public TwitterServices(){}

        public async Task<string> getAuthToken(string name)
        {
            string responseStr="";
            try
            {
                responseStr = await HttpClientSource<string>.RetriveTwitterDataWithPostAsync(name);
                /*
                var responseStr = await HttpClientSource<ArchiveListReq>.CreateOrUpdateItemWithPostAsync("archive-reminders-list.php", request);

                remainderResponse = JsonConvert.DeserializeObject<ArchiveListRes>(responseStr);

                foreach (var item in remainderResponse.response_info)
                {
                    reminderListInfo.Add(new ArchiveListInfo()
                    {
                        registrationDate = item.registration_date,
                        entityTypeId = item.entity_type_id,
                        createdDate = item.created_date,
                        entityName = item.entity_name,
                        stateId = item.state_id,
                        stateName = item.state_name,
                        stateShortCode = item.state_short_code,
                        reminderDate = item.reminder_date,
                        reminderTime = item.reminder_time,
                        isTracked = item.is_tracked,
                        reminderId = item.reminder_id,
                        isTriggered = item.is_triggered,
                        status = item.status,
                        entityImgUrl = item.entity_img_url,
                        isArchived = item.is_archived,
                        domesticForeign = item.domestic_foreign,
                        profitNonProfit = item.profit_nonprofit
                    });
                }
                archiveReminderlist = new ArchiveRemindersList()
                {
                    message = remainderResponse.message,
                    responseInfo = reminderListInfo,
                    status = remainderResponse.status,
                    statusCode = remainderResponse.status_code
                };

            */

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n " + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return responseStr;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CountryBAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

