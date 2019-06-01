sampler TextureSampler : register(s0);

struct VSOutput
{
	float4 position		: SV_Position;
	float4 color		: COLOR0;
	float2 texCoord		: TEXCOORD0;
};


float4 PixelShaderFunction(VSOutput input) : COLOR0
{	
	return tex2D(TextureSampler, input.texCoord) * input.color;
}


technique EffectTechniqueName
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}