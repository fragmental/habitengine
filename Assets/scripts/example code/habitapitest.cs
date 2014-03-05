/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class habitapitest : MonoBehaviour
{
	/*
	GET

/api/v1/status - Returns { "status": "up" } if all is well.
Requires no special options
/api/v1/user - (get user status)
x-api-user: uid
x-api-key: api token
/api/v1/user/tasks
x-api-user: uid
x-api-key: api token
type: habit | daily | todo | reward (optional)
Eg: /api/v1/user/tasks?type=habit
/api/v1/user/task/:id - (get task)
x-api-user: uid
x-api-key: api token
POST

/api/v1/user/task - (create new task)

x-api-user: uid
x-api-key: api token
type: habit | daily | todo | reward (required)
text: This is an example title (required)
value: 0 (required or bad things will happen upon upping/downing)
notes: This is just a simple note (optional)
completed: false (optional, defaults to false)
up: true | false (optional, defaults to true)
down: true | false (optional, defaults to true)
/api/v1/user/tasks/:task/:direction

x-api-user: uid
x-api-key: api token
:task is the task ID.
:direction is either up or down
Note: Despite this being a POST, the request body should be empty.
Note: This route will be shortly moved to /api/v1/user/task/:task/:direction (ie 'task' not 'tasks')
PUT

PUT updates a task. All JSON fields are optional, and existing task values will be used if no new values are supplied.

/api/v1/user/task/:id - (update task)
x-api-user: uid
x-api-key: api token
type: habit | daily | todo | reward
text: This is an example title
value: 0
notes: This is just a simple note
completed: false
up: true | false
down: true | false
DELETE

/api/v1/user/task/:id - (delete task)
x-api-user: uid
x-api-key: api token
curl -X DELETE --compressed -H "Content-Type:application/json" \
  -H "x-api-user: {USER_ID}" \
  -H "x-api-key: {API_TOKEN}" \
  https://habitrpg.com/api/user/task/{TASK_ID}
Responds with 204 No Content
*/
	
	
//string api_url = "https://beta.habitrpg.com/api/v1/user/";
	
//string api_url = "https://habitrpg.herokuapp.com/api/v1/user";
//string api_url = "https://habitrpg.herokuapp.com/api/v1/user/tasks";
	
/* GET
string api_url = "https://habitrpg.herokuapp.com/api/v1/user/tasks";
string status = "status";
string user = "user";
string tasks = "user/tasks";
string getTask = "user/task/:id";
*/
/*POST
string newTask = "user/task"
string "user/tasks" 

/api/v1/user/tasks?type=habit
?up=true
*/
	
	
/*string api_url = "https://beta.habitrpg.com/api/v1/user";
//string api = "https://habitrpg.herokuapp.com/api/v1/user/task/testercules/up";
//string api = "https://habitrpg.herokuapp.com/api/v1/user/task/testercules";
//int i = 0;
	
	IEnumerator Start ()
	{
		
	//	while(i<5)
	//{
	//i++;
		Hashtable habit;
		var request = new HTTP.Request("GET", api_url);
		//set headers
		request.SetHeader("x-api-key", "44c220f6-40a4-45af-a86c-8569710c0133");
		request.SetHeader("x-api-user", "b1fb76bf-bfd4-462d-99c3-419ed9c57a45");
		//request.SetHeader("up", "true");
		request.Send();
				
		while(!request.isDone) yield return new WaitForEndOfFrame();
		if(request.exception != null) 
			{
    		Debug.LogError(request.exception);
			}
			else 
				{
				
    			var response = request.response;
    			//inspect response code
    			Debug.Log(response.status);
    			//inspect headers
    			Debug.Log(response.GetHeader("Content-Type"));
    			//Get the body as a byte array
    			//Debug.Log(response.bytes);
    			//Or as a string
    			Debug.Log(response.Text);
//			Debug.Log(response.tasks);
//				Debug.Log(response.GetHashCode(1,1));
//				Debug.Log (response.GetType);
				Debug.Log(response.headers);
				Debug.Log (response.message);
				Debug.Log (response.progress);
//			Debug.Log (response.ReadFromStream);
//			Debug.Log (response.zipped);
//			Debug.Log (response.Equals);
//			Json JO = new JsonObject(response);
//            habit = JO.Deserialize;
//			Debug.Log(habit.exp);
			//Debug.Log (response.message);
			//Debug.Log (response.progress);
				
				}
		
		
		}
	
	/*Types
	 todo
	 daily
	 
	 rewards
	
	
	
		//Test ();
	//}
	
	/*IEnumerator Test ()
	{
		
			
		var request = new HTTP.Request("GET", api_url2);
		//set headers
		request.SetHeader("x-api-key", "44c220f6-40a4-45af-a86c-8569710c0133");
		request.SetHeader("x-api-user", "b1fb76bf-bfd4-462d-99c3-419ed9c57a45");
		request.Send();
				
		while(!request.isDone) yield return new WaitForEndOfFrame();
		if(request.exception != null) 
			{
    		Debug.LogError(request.exception);
			}
			else 
				{
    			var response = request.response;
    			//inspect response code
    			Debug.Log(response.status);
    			//inspect headers
    			Debug.Log(response.GetHeader("Content-Type"));
    			//Get the body as a byte array
    			//Debug.Log(response.bytes);
    			//Or as a string
    			Debug.Log(response.Text);
				}
	}

	
}

/ *
/ *var express = require('express');
var router = new express.Router();
var user = require('../controllers/user');
var groups = require('../controllers/groups');
var auth = require('../controllers/auth');

/
---------- /api/v1 API ------------
Every url added to router is prefaced by /api/v1
See ./routes/coffee for routes

v1 user. Requires x-api-user (user id) and x-api-key (api key) headers, Test with:
$ cd node_modules/racer && npm install && cd ../..
$ mocha test/user.mocha.coffee
/

var verifyTaskExists = user.verifyTaskExists
var cron = user.cron;

router.get('/status', function(req, res) {
  return res.json({
    status: 'up'
  });
});

/* Scoring/
router.post('/user/task/:id/:direction', auth.auth, cron, user.scoreTask);
router.post('/user/tasks/:id/:direction', auth.auth, cron, user.scoreTask);

/ * Tasks/
router.get('/user/tasks', auth.auth, cron, user.getTasks);
router.get('/user/task/:id', auth.auth, cron, user.getTask);
router.put('/user/task/:id', auth.auth, cron, verifyTaskExists, user.updateTask);
router.post('/user/tasks', auth.auth, cron, user.updateTasks);
router["delete"]('/user/task/:id', auth.auth, cron, verifyTaskExists, user.deleteTask);
router.post('/user/task', auth.auth, cron, user.createTask);
router.put('/user/task/:id/sort', auth.auth, cron, verifyTaskExists, user.sortTask);
router.post('/user/clear-completed', auth.auth, cron, user.clearCompleted);

/ * Items/
router.post('/user/buy/:type', auth.auth, cron, user.buy);

/ * User/
router.get('/user', auth.auth, cron, user.getUser);
router.put('/user', auth.auth, cron, user.updateUser);
router.post('/user/revive', auth.auth, cron, user.revive);
router.post('/user/batch-update', auth.auth, cron, user.batchUpdate);
router.post('/user/reroll', auth.auth, cron, user.reroll);
router.post('/user/buy-gems', auth.auth, user.buyGems);

/ *Groups/
router.get('/groups', auth.auth, groups.getGroups);
router.post('/groups', auth.auth, groups.createGroup);
//TODO:
//GET /groups/:gid (get group)
//PUT /groups/:gid (edit group)
//DELETE /groups/:gid

router.post('/groups/:gid/join', auth.auth, groups.attachGroup, groups.join);
router.post('/groups/:gid/leave', auth.auth, groups.attachGroup, groups.leave);
router.post('/groups/:gid/invite', auth.auth, groups.attachGroup, groups.invite);

//GET /groups/:gid/chat
router.post('/groups/:gid/chat', auth.auth, groups.attachGroup, groups.postChat);
//PUT /groups/:gid/chat/:messageId
//DELETE /groups/:gid/chat/:messageId



module.exports = router;
*/