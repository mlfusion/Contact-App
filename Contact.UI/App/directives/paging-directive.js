
app.directive('pagingDirective', function () {

    return {
        restrict: 'A',
        transclude: true,
        scope: {
            currentPage: '=',
           // itemsPerPage: '=',
            obj: '=',
            fuc: '&'
        },
        templateUrl: '../app/directives/paging-template.html',
        link: function(scope, elem, attrs) {
            scope.itemsPerPage = 5
            console.log(scope.itemsPerPage)
            console.log('pd')
            scope.getFuc = function() {
                 scope.fuc();
               // console.log('log ')
            };
          
            scope.range = function() {
                var rangeSize = 6;
                var ret = [];
                var start;

                start = scope.currentPage;
                if (start > scope.pageCount() - rangeSize) {
                    start = scope.pageCount() - rangeSize + 1;
                }

                for (var i = start; i < start + rangeSize; i++) {
                    ret.push(i);
                }
                return ret;
            };

            scope.prevPage = function() {
                if (scope.currentPage >= 1) {
                    scope.currentPage--;

                    scope.getFuc();
                }
            };

            scope.firstPage = function () {
                scope.currentPage = 1;
                scope.getFuc();
            };

            scope.prevPageDisabled = function() {
                return scope.currentPage === 1 ? "disabled" : "";
            };

            scope.pageCount = function () {
                // update to totalcount
                return Math.ceil(2000 / scope.itemsPerPage) - 1;
            };

            scope.nextPage = function() {
                if (scope.currentPage <= scope.pageCount()) {
                    scope.currentPage++;

                    scope.getFuc();
                }
            };

            scope.lastPage = function() {
                scope.currentPage = scope.pageCount();
                scope.getFuc();
            };

            scope.nextPageDisabled = function() {
                return scope.currentPage === scope.pageCount() ? "disabled" : "";
            };

            scope.setPage = function(n) {
                scope.currentPage = n;
                scope.getFuc();
            };
        }
    };

})