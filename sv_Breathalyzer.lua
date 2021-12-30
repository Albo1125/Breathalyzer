local limitnumber = 35
local limitunit = "µg/100mL"

function breathafunc(source, args, rawCommand)
	if isAuthorized(source) and tablelength(args) == 1 then
		local target = tonumber(args[1])
		local name = GetPlayerName(source)
		if (name ~= nil and GetPlayerName(target) ~= nil) then
			TriggerClientEvent("BTZ:ReceiveTest", target, source, name, tostring(limitnumber), limitunit)
			TriggerClientEvent('chatMessage', source, "BREATHALYZER", {255,255,0}, "Breathalyzer test in progress (limit: "..tostring(limitnumber).. " ".. limitunit..").")
		end
	end
end

RegisterCommand('breathalyze', breathafunc, false)
RegisterCommand('breathalyse', breathafunc, false)
RegisterCommand('breatha', breathafunc, false)

function breathfunc(source, args, rawCommand)
	if tablelength(args) == 1 then
		local reading = tonumber(args[1])
		local name = GetPlayerName(source)
		if (reading ~= nil) then
			TriggerClientEvent("BTZ:ProvideSample", source, name, tostring(reading), limitunit)
		end
	end
end

RegisterCommand('breath', breathfunc, false)

function failprovidefunc(source, args, rawCommand)
	local name = GetPlayerName(source)
	TriggerClientEvent("BTZ:FailProvide", source, name)
end

RegisterCommand('failprovide', failprovidefunc, false)

RegisterServerEvent('Breathalyzer:svBreathalyze')
AddEventHandler('Breathalyzer:svBreathalyze', function(playerid, target)
	if isAuthorized(source) then
		TriggerClientEvent("BTZ:ReceiveTest", target, playerid, GetPlayerName(playerid), tostring(limitnumber), limitunit)
		TriggerClientEvent('chatMessage', playerid, "BREATHALYZER", {255,255,0}, "Breathalyzer test in progress (limit: "..tostring(limitnumber).. " ".. limitunit..").")
	end
end)

RegisterServerEvent("BTZ:SendMessage")
AddEventHandler("BTZ:SendMessage", function(targetid, message)
	TriggerClientEvent('chatMessage', targetid, "BREATHALYZER", {255,255,0}, message)
end)

function tablelength(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end