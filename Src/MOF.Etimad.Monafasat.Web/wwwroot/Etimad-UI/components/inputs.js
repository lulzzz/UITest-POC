Vue.component('input-text', {
   props: ['value', 'title', 'requierd', 'disabled','inptype'],
   template: '#Etd-input-box'
});
Vue.component('input-radio-checkbox', {
   props: ['value', 'title', 'name', 'requierd', 'disabled', 'checked', 'options','inptype'],
   template: '#Etd-input-radio-checkbox',
   data() {
      return {
         selectedOption: []
      }
   }
});
Vue.component('input-select', {
   props: ['title', 'name', 'requierd', 'disabled', 'options','value', 'selected'],
   template: '#Etd-select',
   data() {
      return {
         selectedOption: []
      }
   },
   computed: {
      selectedOptions: {
         get: function() {
            for (var i = 0; i < this.options.length; i++) {
               if (this.options[i].selected == true) {
                 this.selectedOption = this.options[i].value
      
               }
            }
            return this.selectedOption
         },
         set: function (v) {
            this.selectedOption = v
            return this.selectedOption;
         }

      }
   }
})
