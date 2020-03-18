namespace WuxiaWorld.BLL.Services {

    using System.Collections.Generic;

    using DAL.Models;

    using Interfaces;

    public class CommandService : ICommandService {

        public List<CommandModel> Get() {

            var commandList = new List<CommandModel> {
                new CommandModel {
                    IsAdmin = true,
                    Text = "Novel",
                    Children = new List<CommandModel> {
                        new CommandModel {
                            IsAdmin = true,
                            Text = "Create new"
                        },
                        new CommandModel {
                            IsAdmin = true,
                            Text = "Delete existing"
                        },
                        new CommandModel {
                            Text = "List of available novels"
                        }
                    }
                },
                new CommandModel {
                    IsAdmin = true,
                    Text = "Genre",
                    Children = new List<CommandModel> {
                        new CommandModel {
                            IsAdmin = true,
                            Text = "Create new"
                        },
                        new CommandModel {
                            IsAdmin = true,
                            Text = "Delete existing"
                        },
                        new CommandModel {
                            Text = "List of genre",
                            Children = new List<CommandModel> {
                                new CommandModel {
                                    Text = "List of novels that belong to this genre"
                                }
                            }
                        }
                    }
                }
            };

            return commandList;
        }
    }

}