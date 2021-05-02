var template = "<select id='select' ref='select' class='form-control'></select>";

Vue.component('select2', {
   template: template,
   props: {
      options: {
         type: Array,
         required: true
      },
      selected: {
         type: Array,
         required: false
      },
      selectedValue: {
         type: Number,
         required: false
      }
   },
   watch: {
      options: {
         handler: function () {
            // check someData and eventually call
            this.refresh();
         }
      }
   },
   methods: {
      refresh: function () {
         var self = this;
         $(self.$refs.select).empty();
         for (var key in this.options) {
            if (self.options.hasOwnProperty(key)) {

               $(self.$refs.select).append(
                  '<option value="' + self.options[key].value + '">' + self.options[key].text + '</option>'
               );
            }
         }
         $(this.$refs.select).val(this.selected).trigger('change');
      },
      onlyUnique: function (value, index, self) {
         return self.indexOf(value) === index;
      }
   },
   mounted: function () {
      var self = this;
      $(this.$refs.select)
         .select2({})
         .on('change', function (value) {
            //self.set($(self.$refs.select).val())
            if (value.val) {
               var uniqueItems = value.val.filter(self.onlyUnique);
               if (uniqueItems.length > 0)
                  self.$emit('change', uniqueItems);
            }
         }).val(this.selected);
      this.refresh();
   }
});
