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

        var internId = tr.attr("data-id");

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {

            let internData = "";
            let eventData = "";

            $.ajax({
                method: "POST",
                url: "home/getinterndetail",
                data: { id: internId }
            }).done(function (o) {
                $.ajax({
                    method: "POST",
                    url: "home/getinternjoined",
                    data: { internId: internId }
                }).done(function (msg) {

                    internData = JSON.parse(o);
                    d2 = JSON.parse(msg);

                    if (d2.length > 0)
                        $.each(d2, function (i, data) {
                            eventData += "<li>" + "#" + i + 1 + ": " + data + "</li>";
                        })
                    else {
                        eventData = "This person is not involved in any activities."
                    }
                    // Open this row
                    row.child(`<div class="col-sm-3">
                      <h5>Intern info:</h5>
                      <ul class="font-size-sm">
                        <li>Full name: ${internData.fullname}</li>
                        <li>Date of birth: ${internData.birth}</li>
                        <li>Gender: ${internData.gender}</li>
                        <li>Phone number: ${internData.phone}</li>
                        <li>Email: ${internData.email}</li>
                        <li>Join Date: ${internData.joindate}</li>
                      </ul>
                    </div>
                    <div class="col-sm-3">
                      <h5>Relative info:</h5>
                      <ul class="font-size-sm">
                        <li>Time schedule: ${internData.type}</li>
                        <li>Duration: ${internData.duration}</li>
                        <li>Training: ${internData.training}</li>
                        <li>Department: ${internData.department}</li>
                        <li>Organization: ${internData.organization}</li>
                        <li>Mentor: ${internData.mentor}</li>
                      </ul>
                </div>
                <div class="col-sm-3">
                      <h5>Events:</h5>
                      <ul class="font-size-sm">
                        ${eventData}                        
                      </ul>
                </div>`).show();
                    tr.addClass('shown');
                });
            });
        }
    });

    // INITIALIZATION OF SELECT2
    // =======================================================
    $('.js-select2-custom').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init($(this));
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

    // CR:Init for one thing, not two, 3hours to fix bug!
    var orgtable;
    var deptable;
    var poitable;

    ////////////  ORGANIZATION
    $(document).on("click", '#organization-now', function (e) {
        var items = []

        $.ajax({
            method: "GET",
            url: "home/getorganizations",
        }).done(function (json) {
            var parsedJSON = JSON.parse(JSON.stringify(json))

            for (var i = 0; i < parsedJSON.length; i++) {
                items.push(`<tr data-id="${parsedJSON[i].organizationId}">
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
            $("#org-tbody").html(`${items.join("")}`);
            if (!orgtable)
                orgtable = $.HSCore.components.HSDatatables.init($('#orgtable'));

            $.getScript('/js/table-edits.js');
            $('#orgModal').modal('show');
        });
    });


    ////////////  DEPARTMENT
    $(document).on("click", '#department-now', function (e) {
        var items = []

        $.ajax({
            method: "GET",
            url: "home/getdepartments",
        }).done(function (json) {
            var parsedJSON = JSON.parse(JSON.stringify(json))

            for (var i = 0; i < parsedJSON.length; i++) {
                items.push(`<tr data-id="${parsedJSON[i].departmentId}">
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
            $("#dep-tbody").html(`${items.join("")}`);
            if (!deptable)
                deptable = $.HSCore.components.HSDatatables.init($('#deptable'));

            $.getScript('/js/table-edits.js');
            $('#depModal').modal('show');
        });
    });



    ////////////  INTERNSHIPPOINTS
    $(document).on("click", '#point-now', function (e) {
        var items = []

        $.ajax({
            method: "GET",
            url: "home/getinternshippoints",
        }).done(function (json) {
            var parsedJSON = JSON.parse(JSON.stringify(json))

            for (var i = 0; i < parsedJSON.length; i++) {
                items.push(`<tr data-id="${parsedJSON[i].internId}">
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
            $("#poi-tbody").html(`${items.join("")}`);
            if (!poitable)
                poitable = $.HSCore.components.HSDatatables.init($('#poitable'));

            $.getScript('/js/table-edits.js');
            $('#poiModal').modal('show');
        });
    });



    // INITIALIZATION OF NAV SCROLLER
    // =======================================================
    $('.js-nav-scroller').each(function () {
        new HsNavScroller($(this)).init()
    });

    $(document).on("click", '#addibtn', function (e) {
        //var type = $("#cui-submit").text(); //For button

        $('#cui-form').attr('action', '/');

        $('#firstNameLabel').val("");
        $('#lastNameLabel').val("");
        $('#birthLabel').val("");
        $('#emailLabel').val("");
        $('#phoneLabel').val("");
        $('#durationLabel').val("");

        $('#genderLabel').val("").change();
        $('#typeLabel').val("").change();
        $('#orgnLabel').val("").change();
        $('#deptLabel').val("").change();
        $('#trainLabel').val("").change();
    });

});