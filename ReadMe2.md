What was done.


I added a project (CodeChallengWebPages) to the solution.  This is an MVC web progect with Razor 
pages to display the results of the code.

I added Newtonsoft.Json(13.0.3) package to it.



To work it, the solution would need to have both CodeChallenge and CodeChallengeWebPages to be run.

This is done by right click on the solution in the Solution Explorer
Under common properties, startup Project
Select Multiple startup projects option
Change action from none to Start / Start without debugging for CodeChallenge and CodeChallengeWebPages.

Thank you

Jerry DeMas
