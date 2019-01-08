var particleEmitters : ParticleEmitter[];
var scale : float= 1;

@SerializeField
@HideInInspector
private var minsize : float[];

@HideInInspector
@SerializeField
private var maxsize : float[];

@HideInInspector
@SerializeField
private var worldvelocity : Vector3[];

@HideInInspector
@SerializeField
private var localvelocity : Vector3[];

@HideInInspector
@SerializeField
private var rndvelocity : Vector3[];

@HideInInspector
@SerializeField
private var scaleBackUp : Vector3[];

@SerializeField
@HideInInspector
private var firstUpdate = true;

function UpdateScale () {   
    var length = particleEmitters.length;
    
    if(firstUpdate == true){
	minsize = new float[length];
    maxsize = new float[length];
    worldvelocity = new Vector3[length];
  	localvelocity = new Vector3[length];
   	rndvelocity = new Vector3[length];
    scaleBackUp = new Vector3[length];
    }
      
   
    for (i = 0; i < particleEmitters.length; i++) { 
    	if(firstUpdate == true){
           	minsize[i] = particleEmitters[i].minSize;
        	maxsize[i] = particleEmitters[i].maxSize;
        	worldvelocity[i] = particleEmitters[i].worldVelocity;
        	localvelocity[i] = particleEmitters[i].localVelocity;
        	rndvelocity[i] = particleEmitters[i].rndVelocity;
        	scaleBackUp[i] = particleEmitters[i].transform.localScale;
        }
        
        particleEmitters[i].minSize = minsize[i] * scale;
        particleEmitters[i].maxSize = maxsize[i] * scale;
        particleEmitters[i].worldVelocity = worldvelocity[i] * scale;
        particleEmitters[i].localVelocity = localvelocity[i] * scale;
        particleEmitters[i].rndVelocity = rndvelocity[i] * scale;
        particleEmitters[i].transform.localScale = scaleBackUp[i] * scale;
        
    }
	firstUpdate = false;
}


