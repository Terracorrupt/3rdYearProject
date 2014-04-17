
/*
 * GET home page.
 */

exports.index = function(req, res){
  res.render('index', { title: 'Bethselamin Time Trial - Login' });
};

exports.userinfo = function(db){
	
	return function(req, res) {
		var collection = db.get('test1');
		collection.findOne(req.query,function(e,doc){
			console.log(req.query);
            res.render('userinfo', {
                "userinfo" : doc
            });
        });
	};
};

exports.userlist = function(db) {
    return function(req, res) {
        var collection = db.get('test1');
        collection.find({},{},function(e,docs){
            res.render('userlist', {
                "userlist" : docs
            });
        });
    };
};