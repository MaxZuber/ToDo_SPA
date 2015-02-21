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
                    alert("Error - get tasks");
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

            var task = getTask();
            isNaN(task.ID) ? that.AddTask(task) : that.preUpdateTask(task);
            clearTaskForm();
            that.onShowNewTaskPanel();
            
        };

        getTask = function () {
            var taskID = parseInt($("#task-ID").val());
            var taskStatus = parseInt($("#task-Status").val());
            var taskTitle = $("#task-Title").val();
            var taskDescription = $("#task-Description").val();
            var taskDueDate = $("#task-DueDate").val();

            var task = {
                ID: taskID,
                Title: taskTitle,
                Description: taskDescription,
                DueDate: taskDueDate,
                Status:  isNaN(taskStatus) ? 1 : taskStatus
            };

            return task;
        }
        clearTaskForm = function () {
            var taskID = $("#task-ID").val("");
            var taskStatus = $("#task-Status").val("");
            var taskTitle = $("#task-Title").val("");
            var taskDescription = $("#task-Description").val("");
            var taskDueDate = $("#task-DueDate").val("");
        }
        setTaskForm = function (task) {
            $("#task-ID").val(task.ID);
            $("#task-Status").val(task.Status);
            $("#task-Title").val(task.Title);
            $("#task-Description").val(task.Description);
            var date = new Date(task.DueDate);
            $("#task-DueDate").val(date.toDateString());
        }

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

        that.preUpdateTask = function (task) {

            var oldTask; 
            ko.utils.arrayFirst(that.tasks(), function (f) {
                if (f.ID == task.ID) {
                    oldTask = f;
                    that.tasks.remove(f);
                    return true;
                }
            });

            
            task.UserID = null;     //add missed fileds for object map
            task.tblUsers = null;

            that.UpdateTask(task);
        }

        that.UpdateTask = function (task) {
            $.ajax({
                type: "PUT",
                url: "/api/tasks/",
                data: task,
                success: function (data) {
                    console.log("task update - successs");
                    that.tasks.push(task);
                },
                error: function () {
                    console.log("update task - error");
                    alert("Error  - updating to do list");
                }
            });
        };

        that.onChangeStatus = function (task, targetID) {
            that.tasks.remove(task);
            task.Status = targetID;
            that.UpdateTask(task);
        }

        that.ondragstart = function (data, e) {

            console.log('ondragstart');
            var jsonTask = JSON.stringify(data);
            e.dataTransfer.setData('task', jsonTask);
            e.target.SourceId = data.ID;


            return true;
        };

        that.ondragend = function (e) {
            console.log('ondragend');
        };

        that.ondrop = function (data, e) {

            $("#trash-box").removeClass("drag-hover");
            var task = JSON.parse(e.dataTransfer.getData('task'));
            console.log('ondrop js');

            ko.utils.arrayFirst(that.tasks(), function (f) {
                if (f.ID == task.ID) {
                    if (e.target.action == "change-status") {
                        that.onChangeStatus(f, parseInt(e.target.id))
                    }
                    else if (e.target.action == "remove") {
                        that.onRemoveTask(f);
                    }
                    return true;
                }
            });
            return true;
        }

        that.ondragenterTrash = function (data) {
            console.log('dragentertrash');
            //$("#trash").css('background', 'url(/img/trash-opened.png)');
            $("#trash-box").addClass("drag-hover");


            return true;
        }
        that.ondragleaveTrash = function (data) {
            console.log('dragleavetrash');
            //$("#trash").css('background', 'url(/img/trash.png)');
            $("#trash-box").removeClass("drag-hover");
            return true;
        }
        that.ondragoverTrash = function (data) {
            $("#trash-box").addClass("drag-hover");
            console.log('dragoverTrash');
            return true;
        }

        that.dropEditTask = function (d, e) {
            var task = JSON.parse(e.dataTransfer.getData('task'));
            setTaskForm(task);
            console.log("dropEditTask ");
            return true;
        };

        that.dragenterEditTask = function (d, e) {
            if (!$("#newTaskPanel").is(":visible")) {
                that.onShowNewTaskPanel();
            }
            console.log("dragenterEditTask ");
            return true;
        };

        that.dragleaveEditTask = function (d, e) {


            if ($("#newTaskPanel").is(":visible")) {
                that.onShowNewTaskPanel();
            }
            console.log("dragleaveEditTask ");
            return true;
        };
        that.dragoverEditTask = function (d, e) {

            //if ($("#newTaskPanel").is(":visible")) {
            //    that.onShowNewTaskPanel();
            //}

            console.log("dragoverEditTask ");
            return true;
        };
        

        that.onShowNewTaskPanel = function () {
            $("#newTaskPanel").fadeToggle("fast");
            var addButton = $("#btnShowTaskPanel").children();
            if (!addButton.hasClass("glyphicon-plus")) {
                clearTaskForm();
            }
            addButton.toggleClass('glyphicon-plus').toggleClass('glyphicon-minus');
           
        }

        that.onTaskOver = function (data, event) {

        }
    };

    $(function () {
        $("#task-DueDate").datepicker({
            dateFormat: "DD M dd yy"
        });
        $.event.props.push('dataTransfer');
        var model = new taskBoardViewModel();
        model.loadTasks();
        ko.applyBindings(model);
    });
})(jQuery, ko);