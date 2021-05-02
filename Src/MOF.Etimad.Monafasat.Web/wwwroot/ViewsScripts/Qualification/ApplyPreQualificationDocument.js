function ShowHide(hideElement, showElement) {
   $('#' + showElement + '').show(100).removeAttr('hidden');
   $('#' + hideElement + '').hide(100);

}
function checkValidation() {
 
   ShowHide('divContent', 'confirmDiv');
}
