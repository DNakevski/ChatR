//SignalR custom script to update the chat page and send messages

$(function () {

    $.chatPlugin = function (options) {

        var chat = null;
        var isReady = false;

        var settings = $.extend(true, {
            container: "chatContainer",
            messageContainer: "messageContainer",
            nameContainer: "displayname"
        }, options);

        if (typeof (options) == "undefined" || options == null) { options = {}; }

        var n = {
            init: function () {
                chat = $.connection.chatHub;
                // Start the connection.
                $.connection.hub.start().done(function () {
                    isReady = true;
                    $("#" + settings.messageContainer).val('').focus();

                    //get the connected users
                    transport.getConnectedUsers();
                });
            },

            getChat: function () {
                return chat;
            },
        };

        var generator = {

            generateMessage: function(type, name, message)
            {
                var html = "";
                if(type == "sending")
                {
                    html = "<article id='chat-id-1' class='chat-item right'>";
                    html += "<a href='#' class='pull-right thumb-sm avatar'><img src='/Content/ScaleTheme/images/a2.png'></a>";
                    html += "<section class='chat-body'>";
                    html += "<div class='panel bg-light text-sm m-b-none'>";
                    html += "<div class='panel-body'>";
                    html += "<span class='arrow right'></span>";
                    html += "<p class='m-b-none'>" + message + "</p>";
                    html += "</div>";
                    html += "</div>";
                    html += "<small class='text-muted'><i class='fa fa-ok text-success'></i> " + name + "</small>";
                    html += "</section>";
                    html += "</article>";
                }

                if(type== "receiving")
                {
                    html += "<article id='chat-id-1' class='chat-item left'>";
                    html += "<a href='#' class='pull-left thumb-sm avatar'><img src='/Content/ScaleTheme/images/a3.png' class='img-circle'></a>";
                    html += "<section class='chat-body'>";
                    html += "<div class='panel b-light text-sm m-b-none'>";
                    html += "<div class='panel-body'>";
                    html += "<span class='arrow left'></span>";
                    html += "<p class='m-b-none'>"+ message +"</p>";
                    html += "</div>";
                    html += "</div>";
                    html += "<small class='text-muted'>"+ name +"</small>";
                    html += "</section>";
                    html += "</article>";
                }

                return html;
            },

            generateContactInfo: function (name, avatar) {
                var html = "";
                html += "<li class='list-group-item'>";
                html += "<div class='media'>";
                html += "<span class='pull-left thumb-sm avatar'>";
                html += "<img class='bg-white' alt='...' src='/Content/ScaleTheme/images/a3.png'>";
                html += "<i class='away b-black bottom'></i>";
                html += "</span>"
                html += "<div class='media-body'>";
                html += "<div><a href='#'>"+ name +"</a></div>";
                html += "<small class='text-muted'>0 seconds ago</small>";
                html += "</div>"
                html += "</div>";
                html += "</li>";

                return html;
            }
        };

        var uiManager = {
            addMessage: function (message) {
                // Add the message to the page.
                var html = "";
                if (message.Name == $("#" + settings.nameContainer).val()) {
                    html = generator.generateMessage("sending", message.Name, message.MessageText);
                }
                else {
                    html = generator.generateMessage("receiving", message.Name, message.MessageText);
                }

                $("#" + settings.container).append(html);
                //scroll to the bottom of the container
                var scrollTo = $("#" + settings.container)[0].scrollHeight + 50;
                $("#" + settings.container).slimScroll({ scrollTo: scrollTo });
            }
        }

        var transport = {
            sendMessage: function (message) {

                if (isReady) {
                    chat.server.send(message);
                    // Clear text box and reset focus for next comment.
                    $("#" + settings.messageContainer).val('').focus();
                }
                else {
                    alert("The plugin is still not ready!");
                }
                        
            },

            getConnectedUsers: function () {
                chat.server.getAllConectedUsers().done(function (data) {
                    $.each(data, function () {
                        var html = generator.generateContactInfo(this.UserName, this.Avatar);
                        $("#sidebar ul.list-group").append(html);
                    });
                });
            }
        }

        return {
            Init: n.init,
            GetChat: n.getChat,
            SendMessage: transport.sendMessage,
            AddMessage: uiManager.addMessage
        };
    };
           
});