function InternLeave(iid) {

    $.ajax({
        method: "POST",
        url: "home/internleave",
        data: { id: iid }
    }).done(function (msg) {
        alert("Gone: " + msg);
        window.location.reload();
    });
}

function InternUpdate(iid) {

    $.ajax({
        method: "POST",
        url: "home/getinterninfo",
        data: { id: iid }
    }).done(function (o) {
        $('#cui-form').attr('action', '/internupdate/' + iid);
        let obj = JSON.parse(o);

        SetModalData(obj);
        $("#exampleModal").modal();
    });
}

function SetModalData(obj) {
    $('#avatarImg').prop("src", obj.avatar);
    $('#firstNameLabel').val(obj.firstname);
    $('#lastNameLabel').val(obj.lastname);
    $('#birthLabel').val(obj.birth);
    $('#emailLabel').val(obj.email);
    $('#phoneLabel').val(obj.phone);
    $('#durationLabel').val(obj.duration);

    $('#genderLabel').val(obj.gender).change();
    $('#typeLabel').val(obj.type).change();
    $('#organizationLabel').val(obj.organizationid).change();
    $('#departmentLabel').val(obj.departmentid).change();
    $('#trainingLabel').val(obj.trainingid).change();
}

function InternEvaluate(iid) {
    alert("Comming soon...");
}

function ShowInternData(tid, iid) {

    $.ajax({
        method: "POST",
        url: "home/getinterndata",
        data: { trainingId: tid, internId: iid }
    }).done(function (msg) {
        alert(msg);
    });
}

$(document).on('ready', function () {

    //Sync sort,size
    var params = new URLSearchParams(window.location.search);
    $('#datatableEntries').val(params.get("size") ? params.get("size") : 6);
    $('.js-datatable-sort').val(params.get("sort") ? params.get("sort") : 1);
    $('.js-datatable-search').val(params.get("search_on") ? params.get("search_on") : 0);
    $('#datatableSearch').val(params.get("search_string") ? params.get("search_string") : "");


    // INITIALIZATION OF FLATPICKR
    // =======================================================
    $('.js-flatpickr').each(function () {
        $.HSCore.components.HSFlatpickr.init($(this));
    });

    $.HSCore.components.HSFlatpickr.init($('#js-flatpickr-disabling-dates'), {
        disable: [
            function (date) {
                // return true to disable
                return (date.getDay() === 0 || date.getDay() === 6);
            }
        ],
    });


    // INITIALIZATION OF UNFOLD
    // =======================================================
    $('.js-hs-unfold-invoker').each(function () {
        var unfold = new HSUnfold($(this)).init();
    });


    // INITIALIZATION OF FILE ATTACH
    // =======================================================
    $('.js-file-attach').each(function () {
        var customFile = new HSFileAttach($(this)).init();
    });


    // INITIALIZATION OF MASKED INPUT
    // =======================================================
    $('.js-masked-input').each(function () {
        var mask = $.HSCore.components.HSMask.init($(this));
    });


    // INITIALIZATION OF SELECT2
    // =======================================================
    $('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init($(this));
    });


    // INITIALIZATION OF DATATABLES
    // =======================================================
    var datatable = $.HSCore.components.HSDatatables.init($('#datatable'), {
        language: {
            zeroRecords: '<div class="text-center p-4">' +
                '<img class="mb-3" src="/img/sorry.svg" alt="Image Description" style="width: 7rem;">' +
                '<p class="mb-0">No data to show</p>' +
                '</div>'
        }
    });



    // ADD EVENT LISTENER FOR OPENING AND CLOSING DETAILS
    // =======================================================
    $('#datatable tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = datatable.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(`<div class="col-sm-9">
                      <h5>Password requirements:</h5>
                      <p class="font-size-sm mb-2">Ensure that these requirements are met:</p>
                      <ul class="font-size-sm">
                        <li>Minimum 8 characters long - the more, the better</li>
                        <li>At least one lowercase character</li>
                        <li>At least one uppercase character</li>
                        <li>At least one number, symbol, or whitespace character</li>
                      </ul>
                    </div>`).show();
            tr.addClass('shown');
        }
    });

    $('.js-datatable-filter').on('change', function () {
        var $this = $(this),
            elVal = $this.val(),
            targetColumnIndex = $this.data('target-column-index');

        datatable.column(targetColumnIndex).search(elVal).draw();
    });

    $('#datatableSearch').on('keyup', function (e) {
        if (event.keyCode != 13)
            return;

        var searchOn = $('.js-datatable-search').val();

        if (searchOn === '0') {
            alert('You must select the column to search for first!')
            return;
        }

        var params = new URLSearchParams(window.location.search);

        var $this = $(this),
            elVal = $this.val();

        var params = new URLSearchParams(window.location.search);
        params.set('search_string', elVal);
        params.set('search_on', searchOn);

        window.location = "?" + params.toString();
    });

    $('.js-datatable-sort').on('change', function () {
        var $this = $(this),
            elVal = $this.val();

        var params = new URLSearchParams(window.location.search);
        params.set('sort', elVal);

        window.location = "?" + params.toString();
    });

    $('#datatableEntries').on('change', function () {
        var $this = $(this),
            elVal = $this.val();

        var params = new URLSearchParams(window.location.search);
        params.set('size', elVal);

        window.location = "?" + params.toString();
    });

    $(document).on("click", '#addi-btn', function (e) {
        //var type = $("#cui-submit").text(); //For button

        $('#cui-form').attr('action', '/');

        $('#firstNameLabel').val("");
        $('#lastNameLabel').val("");
        $('#birthLabel').val("");
        $('#emailLabel').val("");
        $('#phoneLabel').val("");
        $('#durationLabel').val("");

        $('#genderLabel').val('').change();
        $('#typeLabel').val('').change();
        $('#orgnLabel').val('').change();
        $('#deptLabel').val('').change();
        $('#trainLabel').val('').change();
    });



    ////////////  ORGANIZATION
    $(document).on("click", '#organization-now', function (e) {
        $('#modal-now').removeClass('modal-md')
        $('#modal-now').addClass('modal-xl')
        var items = []

        $.ajax({
            method: "GET",
            url: "home/getorganizations",
        }).done(function (json) {
            var parsedJSON = JSON.parse(JSON.stringify(json))

            for (var i = 0; i < parsedJSON.length; i++) {
                items.push(`<tr id="org-${parsedJSON[i].organizationId}">
                <td data-field="index">${parsedJSON[i].organizationId}</td>
                <td data-field="name">${parsedJSON[i].orgName}</td>
                <td data-field="phone">${parsedJSON[i].orgPhone}</td>
                <td data-field="address">${parsedJSON[i].orgAddress}</td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-danger btn-icon btn-sm">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>
                </td>
                </tr>`);
            }
            $("#modal-now-page").html(`<!-- Table -->
            <div class="table-responsive datatable-custom">
                <table id="tboo" class="js-editable-table table-hover table table-md table-borderless table-thead-bordered table-nowrap table-align-middle"
data-hs-datatables-options='{
                     "columnDefs": [{
                        "targets": [4],
                        "orderable": false
                      }],
                     "order": []
               }'>    
    <colgroup>
       <col span="1" style="width: 5%;">
       <col span="1" style="width: 25%;">
       <col span="1" style="width: 16%;">
       <col span="1" style="width: 50%;">
       <col span="1" style="width: 4%;">
    </colgroup>

<thead class="">
                        <tr>
                            <th>Index</th>
                            <th>Name</th>
                            <th>Phone</th>
                            <th>Address</th>
                            <th>Action</th>
                        </tr>
                    </thead>
            
                    <tbody>
            ${items.join("")}
                    </tbody>
                </table>
            </div>
            <!-- End Table -->`);
        });

        $('#exampleModalCenter').modal('show');
        $.getScript('/js/table-edits.js');
    });



    ////////////  DEPARTMENT
    $(document).on("click", '#department-now', function (e) {
        $('#modal-now').removeClass('modal-xl')
        $('#modal-now').addClass('modal-md')
        var items = []

        $.ajax({
            method: "GET",
            url: "home/getdepartments",
        }).done(function (json) {
            var parsedJSON = JSON.parse(JSON.stringify(json))

            for (var i = 0; i < parsedJSON.length; i++) {
                items.push(`<tr id="dep-${parsedJSON[i].departmentId}">
                <td data-field="index">${parsedJSON[i].departmentId}</td>
                <td data-field="name">${parsedJSON[i].depName}</td>
                <td data-field="location">${parsedJSON[i].depLocation}</td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-danger btn-icon btn-sm">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                </td>
                </tr>`);
            }
            $("#modal-now-page").html(`<!-- Table -->
            <div class="table-responsive datatable-custom">
                <table id="tboo" class="js-editable-table table-hover table table-md table-borderless table-thead-bordered table-nowrap table-align-middle"
data-hs-datatables-options='{
                     "columnDefs": [{
                        "targets": [3],
                        "orderable": false
                      }],
                     "order": []
               }'>
    <colgroup>
       <col span="1" style="width: 4%;">
       <col span="1" style="width: 34%;">
       <col span="1" style="width: 58%;">
       <col span="1" style="width: 4%;">
    </colgroup>

 <thead class="">
                        <tr>
                            <th>Index</th>
                            <th>Name</th>
                            <th>Location</th>
                            <th>Action</th>
                        </tr>
                    </thead>
            
                    <tbody>
            ${items.join("")}
                    </tbody>
                </table>
            </div>
            <!-- End Table -->`);
        });

        $('#exampleModalCenter').modal('show');
        $.getScript('/js/table-edits.js');
    });



    ////////////  INTERNSHIPPOINTS
    $(document).on("click", '#point-now', function (e) {
        $('#exampleModalLongTitle').html('Departments')
        $('#modal-now').removeClass('modal-md')
        $('#modal-now').addClass('modal-xl')
        var items = []

        $.ajax({
            method: "GET",
            url: "home/getinternshippoints",
        }).done(function (json) {
            var parsedJSON = JSON.parse(JSON.stringify(json))

            for (var i = 0; i < parsedJSON.length; i++) {
                items.push(`<tr id="dep-${parsedJSON[i].internId}">
                <td data-field="index">${parsedJSON[i].internId}</td>
                <td data-field="techskill">${parsedJSON[i].technicalSkill}</td>
                <td data-field="softskill">${parsedJSON[i].softSkill}</td>
                <td data-field="attitude">${parsedJSON[i].attitude}</td>
                <td data="score"><i class="tio-star text-warning mr-1"></i> ${parsedJSON[i].score}</td>
                <td data="passed">
                    <span class="badge badge-soft-success p-1">${parsedJSON[i].passed}</span
                </td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-danger btn-icon btn-sm">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                </td>
                </tr>`);
            }
            $("#modal-now-page").html(`<!-- Table -->
            <div class="table-responsive datatable-custom">
                <table id="tboo" class="js-editable-table table-hover table table-md table-borderless table-thead-bordered table-nowrap table-align-middle"
data-hs-datatables-options='{
                     "columnDefs": [{
                        "targets": [5,6],
                        "orderable": false
                      }],
                     "order": []
               }'>
    <colgroup>
       <col span="1" style="width: 15%;">
       <col span="1" style="width: 15%;">
       <col span="1" style="width: 15%;">
       <col span="1" style="width: 15%;">
       <col span="1" style="width: 15%;">
       <col span="1" style="width: 15%;">
       <col span="1" style="width: 10%;">
    </colgroup>

 <thead class="">
                        <tr>
                            <th>Intern ID</th>
                            <th>Technical Skill</th>
                            <th>Soft Skill</th>
                            <th>Attitude</th>
                            <th>Score</th>
                            <th>Passed</th>
                            <th>Action</th>
                        </tr>
                    </thead>
            
                    <tbody>
            ${items.join("")}
                    </tbody>
                </table>
            </div>
            <!-- End Table -->`);
        });

        $('#exampleModalCenter').modal('show');
        $.getScript('/js/table-edits.js');
    });

    // INITIALIZATION OF NAV SCROLLER
    // =======================================================
    $('.js-nav-scroller').each(function () {
        new HsNavScroller($(this)).init()
    });
});