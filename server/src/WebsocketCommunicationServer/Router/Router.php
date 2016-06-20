<?php

namespace AllyTalks\WebsocketCommunicationServer\Router;

use AllyTalks\WebsocketCommunicationServer\Client\Client;
use AllyTalks\WebsocketCommunicationServer\CommServer;
use Ratchet\ConnectionInterface;
use YaLinqo\Enumerable;

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
            default:
                throw new \RuntimeException('Unsupported message type.');
        }
    }

    public function processRegularMessage($sender, $message)
    {
        switch ($message['type']) {
            case 'message' :

                $token = $message['token'];
                $receiver = $message['receiver'];

                /** @var Client $from */
                $from = Enumerable::from($this->server->getClients())->where(
                    function(Client $c) use ($sender) {
                        return $c->getResourceId() === $sender->resourceId;
                    }
                )->firstOrDefault();

                if (!$from) {
                    throw new \RuntimeException('Wrong client.');
                }

                if ($from->getUser()->getToken() !== $token) {
                    throw new \RuntimeException('Incorrect token.');
                }

                /** @var Client $to */
                $to = Enumerable::from($this->server->getClients())->where(
                    function(Client $c) use ($receiver) {
                        return $c->getUser()->getLogin() === $receiver;
                    }
                )->firstOrDefault();

                if (!$to) {
                    throw new \RuntimeException('Receiver is offline.');
                }

                $to->getConnection()->send(json_encode([
                    'sender' => $from->getUser()->getLogin(),
                    'type' => 'message',
                    'message' => $message['message']
                ]));

                break;

            default:
                throw new \RuntimeException('Unsupported message type.');
        }
    }
}