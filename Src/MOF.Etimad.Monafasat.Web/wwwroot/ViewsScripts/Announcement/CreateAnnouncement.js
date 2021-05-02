var modelObj = {
   insideKsa: true,
   areasSelected : null,
   countriesSelected : null,
   activitiesSelected : null,
   constructionsSelected : null,
   maintenanceSelected : null,
};


function InitiateCreateAnnouncementModel(obj) {
   modelObj = obj;
} 

var obj = new Vue({
   el: '#app',
   data() {
      return {
         areas: [],
         countries: [],
         constructionWorks: [],
         maintenanceWorks: [],
         activities: [],
         insideKsa: modelObj.insideKsa,
         areasSelected: modelObj.areasSelected,
         countriesSelected: modelObj.countriesSelected,
         activitiesSelected: modelObj.activitiesSelected,
         constructionsSelected: modelObj.constructionsSelected,
         maintenanceSelected: modelObj.maintenanceSelected,
         testseleted: [],
      }
   },
   created() {
      axios.get("/Lookup/GetCountriesync")
         .then(res => {
            this.countries = res.data;
            setTimeout(function () {
               $('#contriesSelect').selectpicker('refresh');
            }, 1000);
            //$(this.$refs.countriesSelect).dropdown('refresh');
         })
         .catch(res => { console.log(res) });
      axios.get("/Lookup/GetActivitiesAsync")
         .then(res => {
            this.activities = res.data;
            setTimeout(function () {
               $('#activitiesSelect').selectpicker('refresh');
            }, 1000);
            // $(this.$refs.activitiesSelect).dropdown('refresh');
         })
         .catch(res => { console.log(res) });
      axios.get("/Lookup/GetAreasAsync")
         .then(res => {
            this.areas = res.data
            setTimeout(function () {
               $('#areasSelect').selectpicker('refresh');
            }, 1000);
            //$(this.$refs.areasSelect).dropdown('refresh');
         })
         .catch(res => { console.log(res) });

      axios.get("/Lookup/GetConstractionWorkAsync")
         .then(res => {
            this.constructionWorks = res.data
            setTimeout(function () {
               $('#constructionSelect').selectpicker('refresh');
            }, 1000);
            //$(this.$refs.constructionSelect).dropdown('refresh');
         })
         .catch(res => { console.log(res) });
      axios.get("/Lookup/GetmaintenanceWorksAsync")
         .then(res => {
            this.maintenanceWorks = res.data
            setTimeout(function () {
               $('#maintenanceSelect').selectpicker('refresh');
            }, 1000);
            //$(this.$refs.maintenanceSelect).dropdown('refresh');
         })
         .catch(res => { console.log(res) });

   },
   components: {

   }
})
