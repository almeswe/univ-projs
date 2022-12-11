package main.code.com.controller.command.impl.transition;

import main.code.com.controller.command.Command;
import main.code.com.controller.command.CommandResult;
import main.code.com.controller.command.CommandResultType;
import main.code.com.controller.context.RequestContext;
import main.code.com.controller.context.RequestContextHelper;


import javax.servlet.http.HttpServletResponse;

public class GoToChangeApartmentStatusCommand implements Command {
    private static final String PAGE = "WEB-INF/view/changeApartmentStatus.jsp";



    @Override
    public CommandResult execute(RequestContextHelper helper, HttpServletResponse response) {
        RequestContext requestContext = helper.createContext();


        helper.updateRequest(requestContext);
        return new CommandResult(PAGE, CommandResultType.FORWARD);
    }
}
