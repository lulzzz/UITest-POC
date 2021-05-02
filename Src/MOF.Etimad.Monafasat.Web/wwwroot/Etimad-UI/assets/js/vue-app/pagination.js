var template = "<div class='text-center'><nav aria-label='Page navigation' style='display: inline-block;'><ul v-if='totalPages >1' class='pagination pagination-primary'><li class='page-item'><button type='button' class='page-link' :disabled='(currentPage) == 1'@click='fetchData(resource_url, 1)' aria-label='[First]'><span aria-hidden='true'>&laquo;&laquo;</span><div class='ripple-container'></div></button></li><li class='page-item'><button type='button' class='page-link' :disabled='(currentPage-1) == 0' @click='fetchData(resource_url, currentPage-1)' aria-label='Previous'><span aria-hidden='true'>&laquo;</span><div class='ripple-container'></div></button></li><li class='page-item' v-for='page in getPages(currentPage).numeric' @click='fetchData(resource_url, page)' :class='{active: currentPage == page}'><a href='javascript:void(0);' class='page-link'>{{page}}</a></li><li class='page-item'><button type='button' class='page-link' :disabled='(currentPage+1) > totalPages' @click='fetchData(resource_url, currentPage+1)' aria-label='Next'><span aria-hidden='true'>&raquo;</span><div class='ripple-container'></div></button></li><li class='page-item'><button type='button' class='page-link' :disabled='(currentPage) == totalPages' @click='fetchData(resource_url, totalPages)'  aria-label='Last'><span aria-hidden='true'>&raquo;&raquo;</span><div class='ripple-container'></div></button></li></ul><span v-else-if=''></span></nav><div id='loader' v-show='showLoader'><div class='modal-backdrop fade in' style='z-index:1060;'></div><div style='position: fixed; left: 50%; top: 30%; z-index: 1061'><div style='position: relative; left: -50%;'><div class='waiting-modal'><img src='/Etimad-UI/assets/imgs/loader.gif' alt='' title='' /><span>{{ waitingstr}}</span></div></div></div></div></div></div>";


Vue.component('pagination', {
    template: template,
    props: {
        resource_url: {
            type: String,
            required: true
        },
        visible_pages: {
            type: Number,
            required: true
        },
        submit_form: {
            type: Boolean,
            required: false
        },
        custom_template: '',
        options: {
            type: Object,
            required: false,
            default: function _default() {
                return {};
            }
        },
        auto_load: true
    },
    data: function data() {
        return {

            totalCount: '',
            pageSize: '',
            totalPages: '',
            currentPage: '',
            queryString: '',
            m_visible_pages: this.visible_pages,
            showLoader: false,
            config: {
                data: 'data',
                totalCount: 'totalCount',
                pageSize: 'pageSize',
                currentPage: 'currentPage',
                queryString: 'queryString',

            }
        };
    },
    computed: {
        waitingstr: function () {
            if (getCookie('language') == 'en-US') {
                return 'Please Wait ...'
            } else {
                return 'الرجاء الإنتظار ...'
            }
        }
    },
    methods: {

        fetchData: function (pageUrl, page) {
            try {
              //  debugger;
                if (this.submit_form  && page != undefined) {
                     $("#form0").find('.validation-error').html('');
                    $("#form0").find('[type=submit]').click();
                    var errorsCount = $("#form0").find('.validation-error:not(:empty)');
                    if (errorsCount.length != 0) {
                        return false;
                    }

                }

            } catch (e) {
                console.log();
            }
            

            debugger;
            //this.queryString = $('form').serialize();
            this.m_visible_pages = this.visible_pages;
            pageUrl = pageUrl || this.resource_url;

            if (!pageUrl) return;
            this.$emit("request_start");
            this.showLoader = true;

            if (pageUrl.indexOf('?') !== -1)
                pageUrl = pageUrl + "&pageNumber=" + page;
            else
                pageUrl = pageUrl + "?pageNumber=" + page;
            //if (this.queryString.length > 0) {
            //    pageUrl = this.resource_url +'?'+ this.updateQueryStringParameter(this.queryString, 'PageNumber', page);
            //}

            var self = this;
            $.ajax({
                url: pageUrl,
                type: 'get',
                cache: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest.responseText == "Logout") { window.location = '/account/logout'; return; }
                    self.$emit("request_error", textStatus);
                    self.showLoader = false;
                },
                success: function (response) {
                    self.$emit("request_finish", response);
                    self.handleResponseData(response);
                    self.showLoader = false;
                }
            });
        },
        handleResponseData: function handleResponseData(response) {
            this.makePagination(response);
            var data = getNestedValue(response, this.config.data);
            this.$emit('update', data);
        },
        makePagination: function makePagination(data) {
            this.totalCount = getNestedValue(data, this.config.totalCount);
            this.queryString = getNestedValue(data, this.config.queryString);
            this.pageSize = getNestedValue(data, this.config.pageSize);
            this.totalPages = Math.floor((this.totalCount + this.pageSize - 1) / this.pageSize);
            this.currentPage = getNestedValue(data, this.config.currentPage);

            if (this.totalPages < this.m_visible_pages)
                this.m_visible_pages = this.totalPages;
        },
        initConfig: function initConfig() {
            this.config = merge_objects(this.config, this.options);
        },
        getPages: function (currentPage, index) {

            var pages = [];
            var half = Math.floor(this.m_visible_pages / 2);
            var start = currentPage - half + 1 - this.m_visible_pages % 2;
            var end = currentPage + half;

            if (end >= this.totalPages) {
                start = this.totalPages - this.m_visible_pages + 1;
                end = this.totalPages;
            }//Yahya: fixing visibile pages when reach the end.
            //else
            //  end = this.m_visible_pages;

            if (start <= 0) {
                start = 1;

            }

            var itPage = start;
            while (itPage <= end) {
                pages.push(itPage);
                itPage++;
            }

            return { "currentPage": currentPage, "numeric": pages };
        },
        updateQueryStringParameter: function (uri, key, value) {
            var re = new RegExp("([(.){0,1}])" + key + "=.*?(&|$)", "i");
            console.log(uri.match(re));
            var separator = uri.indexOf('?') !== -1 ? "&" : "?";
            if (uri.match(re)) {
                return uri.replace(re, '$1' + key + "=" + value + '$2');
            }
            else {
                return uri + separator + key + "=" + value;
            }
        }

    },
    watch: {
        resource_url: function resource_url() {
            debugger;
            this.fetchData();
        },
        auto_load: function auto_load() {

            this.fetchData();
        }
    },
    created: function created() {
        this.initConfig();

        if (this.auto_load === true || this.auto_load === undefined)
            this.fetchData();
    }
});


function merge_objects(obj1, obj2) {
    var obj3 = {};
    for (var attrname in obj1) {
        obj3[attrname] = obj1[attrname];
    }
    for (var _attrname in obj2) {
        obj3[_attrname] = obj2[_attrname];
    }
    return obj3;
};

function getNestedValue(obj, path) {
    var originalPath = path;
    path = path.split('.');
    var res = obj;
    for (var i = 0; i < path.length; i++) {
        res = res[path[i]];
    }
    if (typeof res === 'undefined') console.log('[pagination] Response doesn\'t contain key ' + originalPath + '!');
    return res;
};

function serializeQueryString(obj, prefix) {
    var str = [];
    for (var p in obj) {
        if (obj.hasOwnProperty(p)) {
            //var k = prefix ? prefix + "[" + p + "]" : p,
            var k = prefix ? prefix + '.' + p : p, v = obj[p];
            v = obj[p];
            str.push(typeof v == "object" ? serializeQueryString(v, k) : encodeURIComponent(k) + "=" + encodeURIComponent(v));
        }
    }
    return str.join("&");
}
