function InternLeave(iid, btn) {

    $.confirm({
        title: 'Delete',
        icon: 'tio-delete-outlined',
        backgroundDismiss: true,
        type: 'red',
        buttons:
        {
            Yes: {
                action: function () {
                    $.post("home/deleteintern", { id: iid })
                        .done(function (data) {
                            $.alert({
                                title: 'Alert!',
                                content: 'Intern removed: ' + data,
                                buttons:
                                {
                                    OK: {
                                        text: 'Refresh',
                                        action: function () {
                                            var tr = btn.closest('tr')
                                            tr.remove()
                                            refreshInternCount()
                                        }
                                    },
                                }
                            });
                        }).fail(function () {
                            alert("Error");
                        });
                }
            },
            No: function () { }
        }
    });
}

function InternUpdate(iid) {

    $.ajax({
        method: "GET",
        url: "home/getinterninfo",
        data: { id: iid }
    }).done(function (o) {
        $('#cui-form').attr('action', '/internupdate/' + iid);
        let obj = JSON.parse(o);

        SetModalData(obj);
        $("#exampleModal").modal();
    });
}

function readAvatarURL(input) {
    var filename = input.files[0]['name'];
    $('#avatarName').val(filename);
}

function SetModalData(obj) {
    $('#avatarImg').attr("src", "img/avatar/" + obj.avatar);

    var filename = obj.avatar;
    var ext = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
    $('#avatarName').val(obj.email + "." + ext);

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
    $.ajax({
        method: "GET",
        url: "home/getpoint",
        data: { id: iid }
    }).done(function (json) {
        if (json == null) { InternEvaluateFirstTime(iid); return; }

        var parsedJSON = JSON.parse(JSON.stringify(json))
        var color;

        if (parsedJSON.passed) color = 'badge-soft-success';
        else color = 'badge-soft-danger';

        let item = `<tr data-id="${parsedJSON.internId}">
                <td data-field="index">${parsedJSON.internId}</td>
                <td data-field="techskill">${parsedJSON.technicalSkill}</td>
                <td data-field="softskill">${parsedJSON.softSkill}</td>
                <td data-field="attitude">${parsedJSON.attitude}</td>
                <td data="score"><i class="tio-star text-warning mr-1"></i> ${parsedJSON.score}</td>
                <td data="passed">
                    <span class="badge ${color} p-1">${parsedJSON.passed}</span
                </td>
                <td data="marker">${parsedJSON.marker}</td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                    <button id="removepoi" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`;

        $("#poi-tbody").html(item);
        if (!poitable)
            poitable = $.HSCore.components.HSDatatables.init($('#poitable'));

        $.getScript('/js/snips/poi-table-edits.js');
        $('#poiModal').modal('show');
    }).fail(function () {
        alert("Error");
    });
}

function InternEvaluateFirstTime(iid) {

    $.confirm({
        title: false,
        content: `<!-- Body -->
<div class="card-body">
    <h4 class="card-header-title mb-4">Internship evaluation<span class="text-black-50" style="float:right">ID: ${iid}</span></h4>
    <div class="row my-2">
        <label for="emailLabel" class="col-sm-7 col-form-label input-label">Technical skill: </label>
        <div class="col-md-4 ms-auto">
            <!-- Quantity Counter -->
            <div class="js-quantity-counter input-group-quantity-counter">
                <input id="techpoint" type="number" class="js-result form-control input-group-quantity-counter-control" min="0" value="1" max="10">
                <div class="input-group-quantity-counter-toggle">
                    <a class="js-minus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-remove"></i>
                    </a>
                    <a class="js-plus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-add"></i>
                    </a>
                </div>
            </div>
            <!-- End Quantity Counter -->
        </div>
    </div>
    <div class="row my-2">
        <label for="emailLabel" class="col-sm-7 col-form-label input-label">Soft skills: </label>
        <div class="col-md-4 ms-auto">
            <!-- Quantity Counter -->
            <div class="js-quantity-counter input-group-quantity-counter">
                <input id="softpoint"  type="number" class="js-result form-control input-group-quantity-counter-control" min="0" value="1" max="10">
                <div class="input-group-quantity-counter-toggle">
                    <a class="js-minus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-remove"></i>
                    </a>
                    <a class="js-plus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-add"></i>
                    </a>
                </div>
            </div>
            <!-- End Quantity Counter -->
        </div>
    </div>
    <div class="row my-2">
        <label for="emailLabel" class="col-sm-7 col-form-label input-label">Attitude: </label>
        <div class="col-md-4 ms-auto">
            <!-- Quantity Counter -->
            <div class="js-quantity-counter input-group-quantity-counter">
                <input id="attipoint" type="number" class="js-result form-control input-group-quantity-counter-control" min="0" value="1" max="10">
                <div class="input-group-quantity-counter-toggle">
                    <a class="js-minus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-remove"></i>
                    </a>
                    <a class="js-plus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-add"></i>
                    </a>
                </div>
            </div>
            <!-- End Quantity Counter -->
        </div>
    </div>
</div>
<!-- End Body -->`,
        backgroundDismiss: true,
        boxWidth: '430px',
        useBootstrap: false,
        onOpenBefore: function () {
            $.getScript('/js/snips/quantity-counter.js');
        },
        onContentReady: function () {
            $.get("home/getpoint", { id: iid })
                .done(function (data) {
                    if (data == null) return;
                    var t = JSON.parse(JSON.stringify(data))

                    $('#techpoint').val(t.technicalSkill)
                    $('#softpoint').val(t.softSkill)
                    $('#attipoint').val(t.attitude)
                });
        },
        buttons:
        {
            Mark: {
                btnClass: 'btn-soft-success',
                action: function () {
                    var values = {
                        "InternId": iid,
                        "SoftSkill": $('#softpoint').val(),
                        "Attitude": $('#attipoint').val(),
                        "TechnicalSkill": $('#techpoint').val(),
                    }

                    $.post("home/evaluateintern", { model: values })
                        .done(function (data) {
                            $.alert({
                                title: 'Alert!',
                                content: 'Result: ' + data,
                                buttons:
                                {
                                    OK: {
                                        text: 'Close',
                                        action: function () {
                                            refreshPointCount();
                                        }
                                    },
                                }
                            });
                        });
                }
            },
            Cancel: function () { }
        }
    });
}


function refreshInternCount() {
    $.get("countbyindex/4", function (data) {
        $('#interns-count').html(data);
    });
}
function refreshPointCount() {
    $.get("countbyindex/6", function (data) {
        $('#points-count').html(data);
    });
}
function refreshTrainingCount() {
    $.get("countbyindex/8", function (data) {
        $('#trainings-count').html(data);
    });
}
function refreshDepartmentCount() {
    $.get("countbyindex/1", function (data) {
        $('#departments-count').html(data);
    });
}
function refreshOrganizationCount() {
    $.get("countbyindex/5", function (data) {
        $('#organizations-count').html(data);
    });
}

function CreateTraining() {
    $.confirm({
        title: false,
        content: `<!-- Body -->
              <div class="card-body">
                <h2 class="card-header-title mb-2">Create a training</h2>
                <!-- Form Group -->
                <div class="form-group">
                  <label for="crtrainingLabel" class="input-label">Training name</label>

                  <div class="input-group input-group-merge">
                    <div class="input-group-prepend">
                      <div class="input-group-text">
                        <i class="tio-briefcase-outlined"></i>
                      </div>
                    </div>
                    <input type="text" class="form-control" name="trainingName" id="crtrainingLabel" placeholder="Enter training name here" aria-label="Enter training name here">
                  </div>
                </div>
                <!-- End Form Group -->

                <!-- Quill -->
                <label class="input-label">Training content</label>

                <div class="quill-custom">
                  <div id="qlToolbarContainer">
                    <div class="row align-items-center">
                      <div class="col-sm">
                        <!-- Toolbar List -->
                        <ul class="list-inline ql-toolbar-list">
                          <li class="list-inline-item">
                            <button class="ql-bold"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-italic"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-underline"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-strike"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-link"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-image"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-blockquote"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-code"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-list" value="bullet"></button>
                          </li>
                        </ul>
                        <!-- End Toolbar List -->
                      </div>
                    </div>
                    <!-- End Row -->
                  </div>

                  <div class="js-quill ql-editor-content"
                       data-hs-quill-options='{
                          "placeholder": "Type your content...",
                          "toolbarBottom": true,
                          "modules": {
                            "toolbar": "#qlToolbarContainer"
                          }
                         }'>
                  </div>
                </div>
                <!-- End Quill -->
              </div>
              <!-- End Body -->`,
        columnClass: 'large',
        container: 'main',
        containerFluid: false,
        dragWindowBorder: false,
        backgroundDismiss: true,
        onOpenBefore: function () {
            $.getScript('/js/snips/quilljs-editor.js');
        },
        buttons:
        {
            Cancel: function () { },
            Create: {
                btnClass: 'btn-soft-success',
                action: function () {
                    var traName = $('#crtrainingLabel').val()
                    var traData = JSON.stringify(quill.getContents())

                    $.post("home/inserttraining", { model: { 'TraName': traName, 'TraData': traData } })
                        .done(function (data) {
                            alert("Result: " + data)
                            refreshTrainingCount()
                        })
                        .fail(function () {
                            alert("Error");
                        });
                }
            },
        }
    });
}

$(document).on('submit', '#cui-form', function () {
    var str = $('#avatarName').val();
    var em = $('#emailLabel').val();
    var ex = str.substring(str.lastIndexOf(".") + 1);

    var imgname = em + "." + ex;
    var img64 = $('#avatarImg').attr("src").split(",").pop();

    $('#avatarName').val(imgname)

    $.post("home/UploadAvatar", {
        ImgStr: img64,
        ImgName: imgname
    }).done(function () {
        alert("Result: Avatar uploaded!");
    }).fail(function () {
        alert("Error");
    });
});

$(document).on('ready', function () {

    //Sync sort,size
    var params = new URLSearchParams(window.location.search);
    $('#datatableEntries').val(params.get("size") ? params.get("size") : 6);
    $('.js-datatable-sort').val(params.get("sort") ? params.get("sort") : 1);
    $('.js-datatable-search').val(params.get("search_on") ? params.get("search_on") : 0);
    $('#datatableSearch').val(params.get("search_string") ? params.get("search_string") : "");

    $('#addibtn').on("click", function (e) {
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

        $('#organizationLabel').val('').change();
        $('#departmentLabel').val('').change();
        $('#trainingLabel').val('').change();
    });

    $.get("home/getpassedcount").done(function (data) {
        $('#passed-count').append(data);
    }).fail(function () {
        alert("Have error when get passed.");
    });

    // INITIALIZATION OF NAV SCROLLER
    // =======================================================
    $('.js-nav-scroller').each(function () {
        new HsNavScroller($(this)).init()
    });


    // INITIALIZATION OF QUILLJS EDITOR
    // =======================================================
    var quill2 = $.HSCore.components.HSQuill.init('.js-quill-modal-eg');


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
        },
        pageLength: 32
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
                method: "GET",
                url: "home/getinterndetail",
                data: { id: internId }
            }).done(function (o) {
                $.ajax({
                    method: "GET",
                    url: "calendar/getinternjoined",
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


    $('.js-datatable-filter').on('change', function () {
        var $this = $(this),
            elVal = $this.val(),
            targetColumnIndex = $this.data('target-column-index');

        datatable.column(targetColumnIndex).search(elVal).draw();
    });


    $('.js-datatable-search').on('change', function () {
        var elVal = $(this).val();

        if (elVal == 0) window.location = "/";
    });

    $('#trainingSelector').on('change', function () {
        var tid = $(this).val();

        $.get("home/gettrainingcontent", {
            id: tid
        }).done(function (data) {
            quill2.setContents(JSON.parse(data), 'api');
        }).fail(function () {
            alert("Error");
        });
    });

    $('#del-training').on('click', function () {
        var tid = $('#trainingSelector').val()

        $.post("home/deletetraining", {
            id: tid
        }).done(function (data) {
            alert(data)
            window.location = "/"
        }).fail(function () {
            alert("Error");
        });
    });

    $('#change-training').on('click', function () {
        var tid = $('#trainingSelector').val()
        var deparray = $('#depSharedSelector').val()

        var traName = $("#trainingSelector option:selected").text();

        $.post("home/updatetraining", {
            model: {
                'TrainingId': tid,
                'TraName': traName,
                'TraData': JSON.stringify(quill2.getContents())
            }
        }).done(function (data) {

            $.post("home/setsharedtraining", {
                sharedId: tid,
                depArray: deparray
            }).done(function (data) {
                alert(data)
            }).fail(function () {
                alert("Error");
            });

        }).fail(function () {
            alert("Error");
        });
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

    $('.paginate_button').on('click', function () {
        var params = new URLSearchParams(window.location.search);
        var curr = $(this).html()

        if (isNaN(curr)) {
            params.set('page', $(this).attr("data-id"));
        }
        else
            params.set('page', curr);

        window.location = "?" + params.toString();
    });

    $('#ad-filter-submit').on('click', function () {
        var passed = $('#pass-filter').val();
        var dateFilter = $('#due-date-mode').val();

        var params = new URLSearchParams(window.location.search);

        if (passed) {
            params.set('on_passed', passed);
        }
        if (dateFilter) {
            params.set('date_filter', dateFilter);

            var st = $('#start-date').val();
            var en = $('#end-date').val();

            if (st)
                params.set('start_date', st);
            if (en)
                params.set('end_date', en);
        }
        if (passed || dateFilter)
            window.location = "?" + params.toString();
    });



    // CR:Init for one thing, not two, 3hours to fix bug!
    var orgtable;
    var deptable;

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
                    <button type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>
                    <button id="removeorg" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`);
            }
            $("#org-tbody").html(`${items.join("")}`);
            if (!orgtable)
                orgtable = $.HSCore.components.HSDatatables.init($('#orgtable'));

            $.getScript('/js/snips/org-table-edits.js');
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
                    <button type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                    <button id="removedep" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`);
            }
            $("#dep-tbody").html(`${items.join("")}`);
            if (!deptable)
                deptable = $.HSCore.components.HSDatatables.init($('#deptable'));

            $.getScript('/js/snips/dep-table-edits.js');
            $('#depModal').modal('show');
        });
    });



    ////////////  TRAINING
    $(document).on("click", '#training-now', function (e) {
        var options = [];
        $.get("home/gettrainings").done(function (data) {
            var parsedJSON = JSON.parse(JSON.stringify(data))
            for (var i = 0; i < parsedJSON.length; i++) {
                options.push(`<option value="${parsedJSON[i].trainingId}">${parsedJSON[i].traName}</option>`);
            }
            $('#trainingSelector').html(options.join(""))
            $('#traModal').modal('show');
        }).fail(function () {
            alert("Error");
        });
    });


    $(document).on("click", '#removedep', function (e) {
        var tr = $(this).closest('tr');
        var id = tr.attr("data-id");

        $.post("home/deletedepartment", {
            id: id
        }).done(function (data) {
            alert("Result: " + data);
            tr.remove()
            refreshDepartmentCount()
        }).fail(function () {
            alert("Error");
        });
    });


    $(document).on("click", '#removeorg', function (e) {
        var tr = $(this).closest('tr');
        var id = tr.attr("data-id");

        $.post("home/deleteorganization", {
            id: id
        }).done(function (data) {
            alert("Result: " + data);
            tr.remove()
            refreshOrganizationCount()
        }).fail(function () {
            alert("Error");
        });
    });


    $(document).on("click", '#removepoi', function (e) {
        var tr = $(this).closest('tr');
        var id = tr.attr("data-id");

        $.post("home/deletepoint", {
            id: id
        }).done(function (data) {
            alert("Result: " + data);
            tr.remove()
            refreshPointCount()
        }).fail(function () {
            alert("Error");
        });
    });
});

