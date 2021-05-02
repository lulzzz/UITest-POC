//! apiRequest.js
//! version : 0.1
//! authors : Abdulaziz Alkharashi

var apiRequest = axios.create({
   headers: {
      'Content-Type': 'application/json',
   }
});
apiRequest.interceptors.request.use(function (request) {
   request.headers.post['X-CSRF-TOKEN'] = getMeta('csrf-token');
   request.headers.post['Content-Type'] = 'application/json';
   return request;
})
apiRequest.interceptors.response.use(
   function (response) {
      handleSuccess(response);
      return response;
   },
   function (error) {
      handleError(error);
      return Promise.reject(error);
   }
)

function getRequest(url, showLoader) {
   if (showLoader) {
      $('#loader').show();
   }
   return new Promise(function (resolve, reject) {
      apiRequest.get(url).then(function (response) {
         if (showLoader)
            $('#loader').hide();
         return resolve(response);
      }).catch(function (error) {
         if (showLoader)
            $('#loader').hide();
         return reject(error);
      })
   });
}

function postRequest(url, body, showLoader = false) {
   const headers = {
      'Content-Type': 'application/json',
   }
   if (showLoader)
      $('#loader').show();
   return new Promise(function (resolve, reject) {
      //console.log(url, body)
      $.post(url, body).done(function (response) {
         if (showLoader)
            $('#loader').hide();
         return resolve(response);
      }).fail(function (error) {
         if (showLoader)
            $('#loader').hide();
         return reject(error);
      })
      //apiRequest.post(url, body, { headers: headers }).then(function (response) {
      //   if (showloader)
      //      $('#loader').hide();
      //   return resolve(response);
      //}).catch(function (error) {
      //   if (showloader)
      //      $('#loader').hide();
      //   return reject(error);
      //})
   });
}


function handleSuccess(response) {
   //console.log(response);
}

// We can here handle all error codes such as
// 404, 401, etc ...
function handleError(error) {
   if (error.response.status == 400) {
      var errorMessage = error.response.data.message ? error.response.data.message : 'حدث خطأ غير متوقع';
      AlertFun(errorMessage, alertMessageType.Danger);
   } else if (error.response.status == 401) {
      window.location = '/account/logout';
   }
}


// Move it to another file.
function getMeta(metaName) {
   const metas = document.getElementsByTagName('meta');
   for (let i = 0; i < metas.length; i++) {
      if (metas[i].getAttribute('name') === metaName) {
         return metas[i].getAttribute('content');
      }
   }
   return '';
}
