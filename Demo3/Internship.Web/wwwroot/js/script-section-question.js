
function CreateQuestion() {
    $.confirm({
        title: false,
        content: `<!-- Body -->
              <div class="card-body">
                <h2 class="card-header-title mb-2">Create one</h2>
                <!-- Form Group -->
                <div class="form-group">
                  <label for="crtrainingLabel" class="input-label">Group</label>

                  <div class="input-group input-group-merge">
                    <div class="input-group-prepend">
                      <div class="input-group-text">
                        <i class="tio-briefcase-outlined"></i>
                      </div>
                    </div>
                    <input type="text" class="form-control" placeholder="Enter group name" aria-label="Enter training name here">
                  </div>
                </div>
                <!-- End Form Group -->
                <div class="form-group">
                  <label for="crtrainingLabel" class="input-label">Question</label>

                  <div class="input-group input-group-merge">
                    <div class="input-group-prepend">
                      <div class="input-group-text">
                        <i class="tio-help-outlined"></i>
                      </div>
                    </div>
                    <input type="text" class="form-control" placeholder="Enter question here" aria-label="Enter training name here">
                  </div>
                </div>
                <!-- End Form Group -->

                <!-- Quill -->
                <label class="input-label">Answers</label>

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
                          "placeholder": "Type your answers...",
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
                    
                }
            },
        }
    });
}