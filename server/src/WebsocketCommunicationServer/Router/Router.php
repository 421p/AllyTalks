<?php

namespace AllyTalks\WebsocketCommunicationServer\Router;

use AllyTalks\WebsocketCommunicationServer\Client\Client;
use AllyTalks\WebsocketCommunicationServer\CommServer;
use Ratchet\ConnectionInterface;

class Router
{
    private $server;

    public function __construct(CommServer $server)
    {
        $this->server = $server;
    }

    public function processServiceMessage(ConnectionInterface $sender, $message)
    {
        switch ($message['type']) {
            case 'auth' :
                $token = $message['token'];
                $user = $this->server->getModel()->getUserByToken($token);

                if ($user->getToken() == $token) {
                    $this->server->addClient(new Client($sender, $user));
                    $sender->send(json_encode([
                        'sender' => 'service',
                        'type' => 'auth',
                        'message' => 'success'
                    ]));
                } else {
                    throw new \RuntimeException('Incorrect token.');
                }

                break;
        }
    }

    public function processRegularMessage($sender, $message)
    {

    }
}