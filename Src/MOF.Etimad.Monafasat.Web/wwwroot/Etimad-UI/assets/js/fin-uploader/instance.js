//var $files = [];
//var $FilesObject = {};
function initUploader(container, textboxID, controller, types, maxsize, onSuccessCallback, onDeleteCallback, _multiple, _itemLimit) {
   _multiple = _multiple == 'undefined' ? "False" : _multiple;
   _itemLimit = _itemLimit == 'undefined' ? 1 : _itemLimit;


   var root1 = $("#root").attr("href"); 
   $(document).ready(function () {

      var $txtInput = $('#' + textboxID);
      var $uploader = $('#' + container).find('.file_uploader');
 
      $uploader.$files = [];

      $uploader.$FilesObject = {};
    
      $uploader.fineUploader({
         request: {
            endpoint: root1 + controller + "/Upload",
            params: { 'name': textboxID }
         },
         multiple: _multiple == "False" ? false : true,
         validation: {
            allowedExtensions: types,
            sizeLimit: maxsize * 1024,//, // 50 kB = 50 * 1024 bytes
            itemLimit: _multiple == "False" ? _itemLimit : ((_itemLimit == 1) ? 100 : _itemLimit)
         },
         text: {
            uploadButton: 'إختر ملفاً..',
            uploadButtonClass: 'btn btn-primary',
            cancelButton: 'إلغاء',
            retryButton: 'إعادة',
            deleteButton: 'حذف',
            failUpload: 'فشل التحميل',
            dragZone: 'اسحب الملفات هنا لرفعها',
            dropProcessing: 'جاري رفع الملفات...',
            formatProgress: "{percent}% من {total_size}",
            waitingForResponse: "جاري التحميل...",
            HintText: 'صيغ الملفات المسموحة: ' + types.join()
         },
         deleteFile: {

            enabled: true,
            endpoint: root1 + controller + "/deleteFile",
            customHeaders: {},
            params: {}
         },
         showMessage: function (message) {
            AlertFun(message, 'danger');

            if (message.toLowerCase() == 'secerror') {
               window.location.reload();
            }
         },


      })
         .on('complete', function (event, id, fileName, responseJSON) {
            $('#loader').hide();
            //$('#loader').remove();
            //$('#LoadingSite').css('opacity', '1');
     
            var $message = $('#' + container).find('.file_uploader_message');
            var deletebtn = $(" <span class='qq-deletebtn glyphicon glyphicon-trash'   title='حذف الملف' alt='حذف الملف' ><i class='material-icons'>delete</i></span>");
            if (_multiple == "False")
            {
               $uploader.$files = [];
               $uploader.$FilesObject = {};
            }
            else
            {
               $uploader.$files = $txtInput.val().split(',')
            }
  
            if (responseJSON.success) {
               var fileLink = root + 'Upload/GetFile?id=' + responseJSON.fileName;
               $uploader.fineUploader('setDeleteFileParams', { fileName: responseJSON.fileName }, id);
               $uploader.$files.push(responseJSON.fileName);
               $uploader.$FilesObject[id] = responseJSON.fileName;

               $txtInput.val( $uploader.$files);

               if ($message.html() == "") {
                  $message.html("<div id = '" + id + "'></div>");
                  $message.find("#" + id).html("<a target='_blank' href='" + fileLink + "' rel='lightbox' alt='عرض الملف' title='عرض الملف' >" + fileName + "</a>").append(deletebtn);
               }
               else {
                  $message.append("<div id = '" + id + "'></div>");
                  $message.find("#" + id).html("<a target='_blank' href='" + fileLink + "' rel='lightbox' alt='عرض الملف' title='عرض الملف' >" + fileName + "</a>").append(deletebtn);
               }

               if (onSuccessCallback) {
                  var fn = new Function("fileName", onSuccessCallback + '(fileName);');
                  fn(responseJSON.fileName);
               }

               if (_multiple == "False") {
                  $uploader.hide();
               }
            }
            else {
               if (responseJSON.innerHTML && responseJSON.innerHTML.toLowerCase().indexOf('maximum request length exceeded') >= 1) {
                  $uploader.fineUploader('itemError', 'sizeError', fileName);
               }
               else if (responseJSON.error) {
                  $uploader.fineUploader('showMessage', responseJSON.error);
               }
            }

            deletebtn.click(function (e) {
               /*e.currentTarget.parentElement.remove();
               $uploader.fineUploader('deleteFile', id);
                 var _filename = $FilesObject[id];
                 var index = $files.indexOf(_filename);
                 $files.splice(index, 1);
                 $txtInput.val($files);
                 if ($message.html() == "") {
                     $txtInput.val("");
                 }
               if (onDeleteCallback) {
                     var fn = new Function("fileName", onDeleteCallback + '(fileName);');
                     fn(responseJSON.fileName);
                 }
                 if (_multiple == "False")
                    $uploader.show();*/
             
               var _filename = $uploader.$FilesObject[id];
               $uploader.$files = $txtInput.val().split(',');
               var index = $uploader.$files.indexOf(_filename);

               var root1 = $("#root").attr("href");
               var obj = {};
               obj.FileName = _filename;
               obj.ModuleType = $('#ModuleType').val();
               var token = $('input[name=__RequestVerificationToken]').val();
               $.post("/Upload/DeleteFile", {
                  model: obj, __RequestVerificationToken: token
               }).done(function (data) {
                  e.currentTarget.parentElement.remove();
                  $uploader.$files.splice(index, 1);
                  $txtInput.val($uploader.$files);
                  if ($message.html() == "") {
                     $txtInput.val("");
                  }

                  if (onDeleteCallback) {
                     var fn = new Function("fileName", onDeleteCallback + '(fileName);');
                     fn(fileName);
                  }
                  if (_multiple == "False")                
                     $uploader.show();
               }).fail(function (e, s, r) {
                  console.log(e);
                  alert("fail");
               });



               //$.ajax({
               //   type: "Post",
               //   url: "/Upload/DeleteFile?fileName=" + _filename,
               //}).done(function () {
               //   e.currentTarget.parentElement.remove();
               //   $uploader.$files.splice(index, 1);
               //   $txtInput.val($uploader.$files);
               //   if ($message.html() == "") {
               //      $txtInput.val("");
               //   }

               //   if (onDeleteCallback) {
               //      var fn = new Function("fileName", onDeleteCallback + '(fileName);');
               //      fn(fileName);
               //   }
               //   if (_multiple == "False")
               //      $uploader.show();
               //}).fail(function (e, s, r) {
               //   console.log(e);
               //   alert("fail");
               //});


            });
         });
       
      try {
         var myLength = $txtInput.val().length

         if (myLength > 0)
            setFileLink(container, textboxID, controller, $txtInput.val(), _multiple);
      }
      catch (ee) {
         console.log("test File Uplosad");
      }
   });
}

function setFileLink(container, textboxID, controller, fileName, _multiple, onDeleteCallback) {

  
   $(document).ready(function () {
      if (fileName.length > 0) {
         if (fileName[0] == ',')
            fileName = fileName.substr(1);
      }
        var $uploader = $('#' + container).find('.file_uploader');
        var $txtInput = $('#' + textboxID);
        var $message = $('#' + container).find('.file_uploader_message');

        var fileName1 = "عرض الملف";

        var deletebtn;

        var finalPath = fileName;

        if (_multiple == "False")
            $uploader.$files = [];
       $uploader.$FilesObject = {};
        $uploader.$files = fileName.split(',');
      
       var index = 100;
      
       for (var i = 0; i < $uploader.$files.length; i++) {
          $uploader.$FilesObject[index] = $uploader.$files[i];
          deletebtn = $("<span class='qq-deletebtn glyphicon glyphicon-trash' title='حذف الملف' alt='حذف الملف' ><i class='material-icons'>delete</i></span>");
        
          if (!$uploader.$files[i].includes("Upload/GetFile"))
             $uploader.$files[i] = "/Upload/GetFile?id=" + $uploader.$files[i];

            if ($message.html() == "") {
                $message.html("<div id = '" + index + "'></div>");
               $message.find("#" + index).html("<a target='_blank' href='" + $uploader.$files[i] + "' rel='lightbox' alt='عرض  ' title='عرض  ' >" + $uploader.$files[i].split(':')[1]  + "</a>").append(deletebtn);
            }
            else {
                $message.append("<div id = '" + index + "'></div>");
               $message.find("#" + index).html("<a target='_blank' href='" + $uploader.$files[i] + "' rel='lightbox' alt='عرض  ' title='عرض  ' >" + $uploader.$files[i].split(':')[1]  + "</a>").append(deletebtn);
            }
            if (_multiple == "False")
                $uploader.hide();
            index++;
           deletebtn.click(function (e) {
              
                
              var id = e.currentTarget.parentElement.id;
              var _filename = $uploader.$FilesObject[id];
              $uploader.$files = $txtInput.val().split(',');
              var index = $uploader.$files.indexOf(_filename);
              
              var root1 = $("#root").attr("href");
              //$.ajax({
              //   type: "Get",
              //   url: "/Upload/DeleteFile?fileName=" + _filename,
              //}).done(function () {
              //   e.currentTarget.parentElement.remove();
              //   $uploader.$files.splice(index, 1);
              //   $txtInput.val($uploader.$files);
              //   if ($message.html() == "") {
              //      $txtInput.val("");
              //   }

              //   if (onDeleteCallback) {
              //      var fn = new Function("fileName", onDeleteCallback + '(fileName);');
              //      fn(fileName);
              //   }
              //   if (_multiple == "False")
              //      $uploader.show();
              //}).fail(function (e, s, r) {
              //   console.log(e);
              //   alert("fail");
              //});
              var obj = {};
              obj.FileName = _filename;
              obj.ModuleType = $('#ModuleType').val();

              var token = $('input[name=__RequestVerificationToken]').val();
              $.post("/Upload/DeleteFile", {
                 model: obj,  __RequestVerificationToken: token
              }).done(function (data) {
                 e.currentTarget.parentElement.remove();
                 $uploader.$files.splice(index, 1);
                 $txtInput.val($uploader.$files);
                 if ($message.html() == "") {
                    $txtInput.val("");
                 }

                 if (onDeleteCallback) {
                    var fn = new Function("fileName", onDeleteCallback + '(fileName);');
                    fn(fileName);
                 }
                 if (_multiple == "False")
                    $uploader.show();
              }).fail(function (e, s, r) {
                 console.log(e);
                 alert("fail");
              });

               
            });
       }



    });
}
