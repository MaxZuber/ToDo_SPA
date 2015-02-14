(function ($, ko) {
    
    var taskBoardViewModel = function () {

        var that = this;
        that.tasks = ko.observableArray();
        that.TaskTitel = ko.observable("New Task Title");
        that.TaskDescription = ko.observable("New task Description");
        that.loadTasks = function () {

            $.ajax({
                url: "/api/tasks",
                //data: comment,
                type: "get",
                success: function (data) {                 
                    console.log("load-tasks:", data);
                    that.tasks(data);
                },
                error: function () {
                    alert("Error");
                }
            });
        };

        that.AddTask = function (task) {
            $.ajax({
                type: "POST",
                url: "/api/tasks",
                data: task,
                success: function (data) {
                    console.log("AddTask:", data);
                    that.tasks.push(data);
                },
                error: function () {
                    console.log("AddTask - error");
                }
            });
        };

        that.onAddTask = function () {
            var task = {
                Title: that.TaskTitel,
                Description: that.TaskDescription,
                Status: 1
            };

            that.AddTask(task);
        };

        that.onRemoveTask = function (task) {
            console.log("onRemoveTask", task);

            $.ajax({
                type: "DELETE",
                url: "/api/tasks/" + task.ID,
                success: function (data) {
                    that.tasks.remove(task);
                },
                error: function () {
                    console.log("onRemoveTask - error");
                }
            });
        };

        //that.allowDrop = function (ev) {
        //    ev.preventDefault();
        //};

        //that.drag = function (ev) {
        //    ev.dataTransfer.setData("text", ev.target.id);
        //}
        //that.drop = function (ev) {
        //    ev.preventDefault();
        //    var data = ev.dataTransfer.getData("text");
        //    ev.target.appendChild(document.getElementById(data));
        //}

        that.ondragstart = function (data, e) {

            console.log('ondragstart');

            e.dataTransfer.setData('text', data.ID);

            return true;
        };

        that.ondragend = function (e) {
            console.log('ondragend');
            //resetUI();
        };

        that.ondrop = function (data, e) {

            $("#trash").css('background', 'url(/img/trash.png)');
            var d = e.dataTransfer.getData('text');
            console.log('ondrop js', d);

            ko.utils.arrayFirst(that.tasks(), function (f) {
                if (f.ID == d) {
                    var ff = f;
                    that.tasks.remove(ff);

                    if (e.target.action == "change-status") {

                        $.ajax({
                            type: "PUT",
                            url: "/api/tasks/",
                            data: ff,
                            success: function (data) {
                                ff.Status = parseInt(e.target.id);
                                that.tasks.push(ff);
                            },
                            error: function () {
                                console.log("update task - error");
                            }
                        });
                    }
                    return true;
                }
            });
            return true;
        }

        that.ondragenterTrash = function (data) {
            console.log('dragentertrash');
            $("#trash").css('background', 'url(/img/trash-opened.png)');
            return true;
        }
        that.ondragleaveTrash = function (data) {
            console.log('dragleavetrash');
            $("#trash").css('background', 'url(/img/trash.png)');
            return true;
        }
        that.ondragoverTrash = function (data) {
            console.log('dragoverTrash');
            return true;
        }

        var _private = {};
        var _public = {};

        _public.init = function () {

        };
    };



    $(function () {
        $.event.props.push('dataTransfer');
        $.event.props.push('pageX');
        $.event.props.push('pageY');
        var model = new taskBoardViewModel();
        model.loadTasks();
        ko.applyBindings(model);
    });
})(jQuery, ko);